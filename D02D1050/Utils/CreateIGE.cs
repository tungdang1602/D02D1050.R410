using D99D0041;
using Lemon3;
using Lemon3.Controls.DevExp;
using Lemon3.Data;
using Lemon3.Functions;
using Lemon3.Resources;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace D31D2150.Utils
{
    class CreateIGE
    {
        /// <summary>
        /// Dùng cho việc sinh IGE (Số phiếu khi người dùng click vào chìa khóa)
        /// </summary>
        public long gnNewLastKey;


        public string CreateIGEVoucherNo(L3LookUpEdit c1Combo, bool FlagSave, string TableName = "D91T0001")
        {
            string strS1 = "";
            string strS2 = "";
            string strS3 = "";
            if (c1Combo.ReturnValue("S1Type") != "0")
                strS1 = FindSxType(c1Combo.ReturnValue("S1Type"), c1Combo.ReturnValue("S1").Trim());
            if (c1Combo.ReturnValue("S2Type") != "0")
                strS2 = FindSxType(c1Combo.ReturnValue("S2Type"), c1Combo.ReturnValue("S2").Trim());
            if (c1Combo.ReturnValue("S3Type") != "0")
                strS3 = FindSxType(c1Combo.ReturnValue("S3Type"), c1Combo.ReturnValue("S3").Trim());

            OutOrderEnum enOutOrderEnum;
            Enum.TryParse(c1Combo.ReturnValue("OutputOrder"), out enOutOrderEnum);

            return IGEVoucherNo(FlagSave, gnNewLastKey, strS1.Trim(), strS2.Trim(), strS3.Trim(), enOutOrderEnum, Convert.ToInt16(c1Combo.ReturnValue("OutputLength")), c1Combo.ReturnValue("Separator").Trim(), TableName);
        }


        public string FindSxType(string nType, string s)
        {
            switch (nType.Trim())
            {
                case "1": // Theo tháng
                    {
                        return L3.TranMonth.ToString("00");
                    }

                case "2": // Theo năm (YYYY)
                    {
                        return L3.TranYear.ToString();
                    }

                case "3": // Theo loại chứng từ
                    {
                        return s;
                    }

                case "4": // Theo đơn vị
                    {
                        return L3.DivisionID;
                    }

                case "5": // Theo hằng
                    {
                        return s;
                    }

                case "6": // Theo năm (YY)
                    {
                        return L3.TranYear.ToString().Substring(2, 2);
                    }

                case "7": // Theo tháng năm (MMYY)
                    {
                        return L3.TranMonth.ToString("00") + L3.TranYear.ToString().Substring(2, 2);
                    }

                case "8": // Theo năm tháng (YYMM)
                    {
                        return L3.TranYear.ToString().Substring(2, 2) + L3.TranMonth.ToString("00");
                    }

                case "9": // Theo năm tháng (YYMMDD)'Update 31/03/2014 for incident 64368 
                    {
                        return DateTime.Now.Year.ToString().Substring(2) + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
                    }

                default:
                    {
                        return "";
                    }
            }
        }
        string IGEVoucherNo(bool bFlagSave, long nNewLastKey, string sStringKey1, string sStringKey2,
            string sStringKey3, D99D0041.OutOrderEnum nOutputOrder, int nOutputLength, string sSeperatorCharacter, string sTableName = "D91T0001")
        {
            return IGEVoucherNo(bFlagSave, nNewLastKey, sStringKey1, sStringKey2, sStringKey3, nOutputOrder, nOutputLength, sSeperatorCharacter, "", sTableName);
        }
        string IGEVoucherNo(bool bFlagSave, long nNewLastKey, string sStringKey1, string sStringKey2,
            string sStringKey3, D99D0041.OutOrderEnum nOutputOrder, int nOutputLength, string sSeperatorCharacter, string VoucherTableName, string sTableName)
        {
            try
            {
                string sIGEVoucherNo = "";
                string KeyString = "";
                long LastKey = 0;

                KeyString = sStringKey1 + sStringKey2 + sStringKey3;

                // Get LastKey from table TableName
                if (nNewLastKey != 0)
                    LastKey = nNewLastKey;
                else
                    LastKey = GetLastKey(KeyString, VoucherTableName, sTableName);

                // Kiem tra chieu dai và lấy chuỗi string của Lastkey
                string LastKeyString;

                LastKeyString = CheckLengthKey(LastKey, sStringKey1, sStringKey2, sStringKey3, sSeperatorCharacter, nOutputLength);

                // Update 21/12/2012: Nếu chiều dài không hợp lệ thì thoát
                if (string.IsNullOrEmpty(LastKeyString))
                    return "";
                else
                    sIGEVoucherNo = Generate(sStringKey1, sStringKey2, sStringKey3, nOutputOrder, sSeperatorCharacter, LastKeyString);


                if (sIGEVoucherNo == "")
                {
                    D99C0008.MsgL3("Lỗi sinh số phiếu tự động (" + sTableName + ")", L3MessageBoxIcon.Err);
                    Lemon3.IO.L3LogFile.WriteLogFile("Loi sinh so phieu cua table " + sTableName, "LogIGEVoucherNo.log");
                    if (bFlagSave)
                        Application.Exit();
                    else
                        return "";
                }

                if (bFlagSave)
                    // Server Tự tăng lên
                    SaveLastKey(sTableName, KeyString, LastKey, 0, VoucherTableName);

                return sIGEVoucherNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Announcement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Lemon3.IO.L3LogFile.WriteLogFile(Convert.ToString(Convert.ToString(Convert.ToString("Loi sinh so phieu (end) cua table " + sTableName + "(") + DateTime.Now.Year) + DateTime.Now.Month.ToString("00")) + DateTime.Now.Date.ToString("00") + ")", "LogIGEVoucherNo.log");
                if (bFlagSave)
                    Application.Exit();
                else
                    return "";
            }
            return "";
        }

        public string Generate(string sS1, string sS2, string sS3, OutOrderEnum sOrder, string sCharacter, string sLastKeyString)
        {
            string strIDKey = "";
            string strIncrement;

            strIncrement = sLastKeyString;

            if (strIncrement == "")
                return "";

            switch (sOrder)
            {
                case OutOrderEnum.lmSSSN:
                    strIDKey = ConcatenateKeys(sS1, sS2, sS3, strIncrement, sCharacter);
                    break;
                case OutOrderEnum.lmSSNS:
                    strIDKey = ConcatenateKeys(sS1, sS2, strIncrement, sS3, sCharacter);
                    break;
                case OutOrderEnum.lmSNSS:
                    strIDKey = ConcatenateKeys(sS1, strIncrement, sS2, sS3, sCharacter);
                    break;
                case OutOrderEnum.lmNSSS:
                    strIDKey = ConcatenateKeys(strIncrement, sS1, sS2, sS3, sCharacter);
                    break;
            }

            return strIDKey;
        }

        public string ConcatenateKeys(string Key1, string Key2, string Key3, string Key4, string sCharacter)
        {
            string sKey1;
            string sKey2;
            string sKey3;
            string sKey4;
            sKey1 = Key1; sKey2 = Key2; sKey3 = Key3; sKey4 = Key4;

            if (sCharacter != "")
            {
                if (sKey1 != "")
                    sKey1 = sKey1 + sCharacter;
                if (sKey2 != "")
                    sKey2 = sKey2 + sCharacter;
                if (sKey3 != "")
                    sKey3 = sKey3 + sCharacter;
                if (sKey4 != "")
                    sKey4 = sKey4 + sCharacter;
            }

            string sConcatenateKeys = "";
            sConcatenateKeys = sKey1 + sKey2 + sKey3 + sKey4;

            if (sCharacter != "")
                return Strings.Left(sConcatenateKeys, Strings.Len(sConcatenateKeys) - Strings.Len(sCharacter));
            else
                return sConcatenateKeys;
        }


        public long GetLastKey(string sKeyString = "", string sTableName = "D91T0001")
        {
            // Update 24/02/2016: Làm theo Incident 84458 
            return GetLastKey(sKeyString, "", sTableName);
        }

        public long GetLastKey(string sKeyString, string VoucherTableName, string sTableName)
        {
            // 'Kiểm tra bảng D91T0000
            // 'Nếu tìm thấy then lấy LastKey
            // 'Nếu không tìm thấy thì insert 1 dòng mới vào
            // Dim sSQL As String
            // sSQL = "SELECT LastKey FROM D91T0000 WHERE TableName ='" & sTable & "'" _
            // & " AND KeyString = '" & sStringCreateKey & "'"
            // Dim sLastKey As String
            // sLastKey = ReturnScalar(sSQL)

            // If sLastKey <> "" Then ' có dữ liệu
            // Return CLng(sLastKey) + 1
            // Else ' Không có dữ liệu
            // sSQL = "INSERT INTO D91T0000 (TableName, KeyString, LastKey)  VALUES ('" & sTable & "', '" & sStringCreateKey & "',0)"
            // ExecuteSQLNoTransaction(sSQL)
            // Return 1
            // End If

            // Update 26/02/2015: Làm theo Incident 71030 
            string sSQL = "----Get Last Key cho So phieu tu dong, neu chua co du lieu thi tra ve 0" + Environment.NewLine;
            sSQL += SQLStoreD91P9111("", VoucherTableName, "", "", "", 1, 1, "", sKeyString, 1, 0, 0, 0, sTableName);

            DataTable dt;

            dt = L3SQLServer.ReturnDataTable(sSQL);
            if (dt.Rows.Count > 0)
                return L3ConvertType.L3Int(dt.Rows[0]["Lastkey"].ToString()) + 1;
            else
                return 1;
        }

        public string SQLStoreD91P9111(string VoucherIGE, string VoucherTableName, string S1, string S2, string S3, int OutputLength, int OutputOrder, string Seperator, string KeyString, byte IsGetLastKey, byte IsSaveNewLastKey, long LastKeyNew, byte IsRefeshLastKey, string TableName)
        {
            // SQLStoreD91P9111("", "", "", "", "", 1, 1, "", sKeyString, 0, 1, nLastKey, iIsRefeshLastKey, sTableName)

            string sSQL = ""; // = "----Tang so phieu tu dong va kiem tra trung phieu tu dong tang" & vbCrLf
            sSQL += "SET NOCOUNT ON " + Environment.NewLine;
            sSQL += "DECLARE @VoucherNo AS VARCHAR(20) " + Environment.NewLine;
            sSQL += "DECLARE @Lastkey AS VARCHAR(20) " + Environment.NewLine;
            sSQL += "Exec D91P9111 ";
            sSQL += L3SQLClient.SQLString(TableName) + L3.COMMA; // TableName, varchar[8], NOT NULL
            sSQL += L3SQLClient.SQLString(S1) + L3.COMMA; // StringKey1, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(S2) + L3.COMMA; // StringKey2, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(S3) + L3.COMMA; // StringKey3, varchar[20], NOT NULL

            sSQL += L3SQLClient.SQLNumber(OutputLength) + L3.COMMA; // OutputLen, int, NOT NULL
            sSQL += L3SQLClient.SQLNumber(OutputOrder) + L3.COMMA; // OutputOrder, int, NOT NULL

            if (Seperator != "")
            {
                sSQL += L3SQLClient.SQLNumber(1) + L3.COMMA; // Seperated, int, NOT NULL
                sSQL += L3SQLClient.SQLString(Seperator) + L3.COMMA; // Seperator, char, NOT NULL
            }
            else
            {
                sSQL += L3SQLClient.SQLNumber(0) + L3.COMMA; // Seperated, int, NOT NULL
                sSQL += L3SQLClient.SQLString("") + L3.COMMA; // Seperator, char, NOT NULL
            }

            sSQL += L3SQLClient.SQLString("") + L3.COMMA; // Temp1, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLString("") + L3.COMMA; // Temp2, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLString("") + L3.COMMA; // Temp3, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(VoucherIGE) + L3.COMMA; // VoucherIGE, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(VoucherTableName) + L3.COMMA;  // VoucherTableName, varchar[20], NOT NULL
            sSQL += " @VoucherNo  OUTPUT " + L3.COMMA + Environment.NewLine; // VoucherNo, varchar[20], NOT NULL
            sSQL += " @Lastkey  OUTPUT " + L3.COMMA + Environment.NewLine; // Lastkey, varchar[20], NOT NULL
            sSQL += L3SQLClient.SQLNumber(0) + L3.COMMA; // IsNotSelect, tinyint, NOT NULL
            sSQL += L3SQLClient.SQLString(L3.DivisionID) + L3.COMMA; // DivisionID, varchar[50], NOT NULL
            sSQL += L3SQLClient.SQLNumber(L3.TranYear) + L3.COMMA;  // TranYear, int, NOT NULL
            sSQL += L3SQLClient.SQLString(KeyString) + L3.COMMA; // KeyString, varchar[50], NOT NULL
            sSQL += L3SQLClient.SQLNumber(IsGetLastKey) + L3.COMMA;  // IsGetLastKey, int, NOT NULL
            sSQL += L3SQLClient.SQLNumber(IsSaveNewLastKey) + L3.COMMA;   // IsSaveNewLastKey, int, NOT NULL
            sSQL += L3SQLClient.SQLNumber(LastKeyNew) + L3.COMMA;  // LastKeyNew, int, NOT NULL
            sSQL += L3SQLClient.SQLNumber(IsRefeshLastKey) + Environment.NewLine;  // IsRefeshLastKey, int, NOT NULL

            if (IsGetLastKey == 0 & IsSaveNewLastKey == 0)
                sSQL += "SELECT @VoucherNo AS VoucherNo,  @Lastkey as Lastkey ";

            return sSQL;
        }

        public string CheckLengthKey(long nLastKey, string sStringKey1, string sStringKey2, string sStringKey3, string sSeperatorCharacter, int nOutputLength)
        {
            int nKeyLength = 0;
            if (sSeperatorCharacter != "")
            {
                if (sStringKey1 != "")
                    nKeyLength = nKeyLength + sStringKey1.Length + sSeperatorCharacter.Length;
                if (sStringKey2 != "")
                    nKeyLength = nKeyLength + sStringKey2.Length + sSeperatorCharacter.Length;
                if (sStringKey3 != "")
                    nKeyLength = nKeyLength + sStringKey3.Length + sSeperatorCharacter.Length;
            }
            else
            {
                if (sStringKey1 != "")
                    nKeyLength = nKeyLength + sStringKey1.Length;
                if (sStringKey2 != "")
                    nKeyLength = nKeyLength + sStringKey2.Length;
                if (sStringKey3 != "")
                    nKeyLength = nKeyLength + sStringKey3.Length;
            }

            if ((nKeyLength + nLastKey.ToString().Length) > nOutputLength)
            {
                AnnouncementLength();
                return "";
            }

            int nLastKeyLength = 0;
            nLastKeyLength = System.Convert.ToInt32(nOutputLength) - nKeyLength - nLastKey.ToString().Length;
            // LastKeyString = Strings.StrDup(nLastKeyLength, "0") & nLastKey


            return Strings.StrDup(nLastKeyLength, "0") + nLastKey;
        }

        public void AnnouncementLength()
        {
            if (L3.Language == EnumLanguage.Vietnamese)
                D99C0008.MsgL3("Chiều dài thiết lập vượt quá giới hạn cho phép." + Environment.NewLine + "Bạn phải thiết lập lại.", L3MessageBoxIcon.Exclamation);
            else
                D99C0008.MsgL3("The lenght setup is off limits." + Environment.NewLine + "You should set again.", L3MessageBoxIcon.Exclamation);
        }

        public void SaveLastKey(string sTableName, string sKeyString, long nLastKey, int iIsRefeshLastKey, string VoucherTableName)
        {
            string strSQL = "----Luu Last Key cho So phieu tu dong" + Environment.NewLine;

            strSQL += SQLStoreD91P9111("", VoucherTableName, "", "", "", 1, 1, "", sKeyString, (byte)0, (byte)1, nLastKey, (byte)iIsRefeshLastKey, sTableName);

            ExecuteSQLNoTransaction(strSQL);
        }

        /// <summary>
        /// Thực thi một chuỗi SQL không dùng Transaction (2 Đối số:  chuỗi SQL và chuỗi kết nối mới (nếu có))
        /// </summary>
        /// <param name="strSQL">Chuỗi SQL để thực thi</param>
        /// <returns>Trả về True nếu thực thi chuỗi SQL thành công, ngược lại trả về False</returns>
        /// <remarks></remarks>
        public bool ExecuteSQLNoTransaction(string strSQL, string sConnectionStringNew = "")
        {
            return ExecuteSQLNoTransaction(strSQL, "", true, sConnectionStringNew);
        }

        public bool ExecuteSQLNoTransaction(string strSQL, string MyMsgErr, bool bUseClipboard, string sConnectionStringNew = "")
        {
            if (Strings.Trim(strSQL) == "")
                return false;
            // Minh Hòa Update 10/08/2012: Đếm số lần bị lỗi
            int iCountError = 0;

            // Update 18/10/2010: Chỉ kiểm tra cho trường hợp nhập liệu Unicode
            // Khi Lưu xuống database nếu chiều dài dữ liệu vượt quá giới hạn cho phép thì không thông báo
            // Bỏ 29/03/2013
            // If gbUnicode Then strSQL = "SET ANSI_WARNINGS OFF " & vbCrLf & strSQL

            // ErrorHandles:
            SqlConnection conn = new SqlConnection(L3.ConnectionString);
            if (sConnectionStringNew != "")
                conn = new SqlConnection(sConnectionStringNew);
            else
                conn = new SqlConnection(L3.ConnectionString);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
        ErrorHandles:
            try
            {
                try
                {

                    conn.Open();
                    // cmd.CommandTimeout = 0
                    if (iCountError > 0)
                        cmd.CommandTimeout = 30;
                    else
                        cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if (iCountError > 0)
                        // conn.ConnectionString = conn.ConnectionString.Replace(gsConnectionTimeout15, gsConnectionTimeout)
                        conn.ConnectionString = L3.ConnectionString.Replace(L3.CONNECTTION_TIME_OUT_15, L3.CONNECTTION_TIME_OUT);

                    return true;
                }
                catch (SqlException ex)
                {
                    // Minh Hòa Update 10/08/2012: Kiểm tra nếu không kết nối được với server thì thông báo để kết nối lại.
                    if (ex.Number == 10054 || ex.Number == 1231 || ex.Message.Contains("Could not open a connection to SQL Server") || ex.Message.Contains("The server was not found or was not accessible") || ex.Message.Contains("A transport-level error"))
                    {
                        if (CheckConnectFailed(ref conn, ref iCountError, "FromExecNoTrans"))
                            goto ErrorHandles;
                    }
                    else if (ex.Number == 1205 || ex.Message.Contains("deadlocked"))
                    {
                        conn.Close();
                        iCountError += 1;
                        Lemon3.IO.L3LogFile.WriteLogFile(strSQL, "Deadlocked (ExecuteSQLNoTransaction)_" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".log");
                        // pauses for five second.
                        System.Threading.Thread.Sleep(3000);

                        goto ErrorHandles;
                    }
                    else
                    {
                        conn.Close();
                        if (bUseClipboard)
                        {
                            Clipboard.Clear();
                            Clipboard.SetText(strSQL);
                        }
                        if (MyMsgErr != "")
                            MsgErr(MyMsgErr);

                        // Update 15/09/2011: Bổ sung kiểm tra mã lỗi từ Server trả ra
                        if (ex.Number >= 50000)
                            MsgErr(ex.Errors[0].Message);
                        else
                            MsgErr("Error when execute SQL in function ExecuteSQL(). Paste your SQL code from Clipboard" + Environment.NewLine + ex.Errors[0].Message);
                        Lemon3.IO.L3LogFile.WriteLogFile(strSQL);
                        return false;
                    }
                }
            }
            catch (Exception ex1)
            {
                MsgErr("Error when execute SQL " + Environment.NewLine + ex1.Message);
                return false;
            }
            return true;
        }

        public bool CheckConnectFailed(ref SqlConnection conn, ref int iCountError, string TypeCall = "", bool bSetConnString = false)
        {
            string sCountError = "";

            if (TypeCall == "FromDataSet")
                sCountError = " (" + iCountError + ")1";
            else if (TypeCall == "FromExecNoTrans")
                sCountError = " (" + iCountError + ")2";
            else
                sCountError = " (" + iCountError + ")";
            Lemon3.IO.L3LogFile.WriteLogFile("gsConnectionString = " + L3.ConnectionString + " And conn.ConnectionString = " + conn.ConnectionString);

            if (D99C0008.Msg(L3Resource.rL3("Khong_ket_noi_duoc_voi_may_chu") + Strings.Space(1) + L3.Server + sCountError + "." + Environment.NewLine + L3Resource.rL3("Ban_co_muon_tiep_tuc_ket_noi_khong"), "Lemon3", L3MessageBoxButtons.YesNo, L3MessageBoxIcon.Err) == DialogResult.Yes)
            {
                // If MessageBox.Show("Kh¤ng kÕt nçi ¢§íc mÀy chï " & gsServer & sCountError & "." & vbCrLf & "BÁn câ muçn tiÕp tóc kÕt nçi kh¤ng?", "Læi", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                // If MessageBox.Show("Connection failed (" & iCountError & ")" & vbCrLf & "Do you want to re_connect?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = DialogResult.Yes Then
                // Gắn lại thời gian ConnectionTimeout để chạy nhanh hơn
                if (iCountError == 0)
                    // If bSetConnString Then
                    conn.ConnectionString = L3.ConnectionString.Replace(L3.CONNECTTION_TIME_OUT, L3.CONNECTTION_TIME_OUT_15);

                iCountError += 1;
                return true;
            }
            else
            {
                return false;
                // MsgErr("Ch§¥ng trØnh kÕt thòc.")
                D99C0008.MsgL3(L3Resource.rL3("Chuong_trinh_ket_thuc"));
                conn.Close();
                conn.Dispose();
                Application.Exit();
            }
        }

        /// <summary>
        ///     ''' Hiển thị báo lỗi trong quá trình coding
        ///     ''' </summary>
        ///     ''' <param name="Text">Chuỗi báo lỗi</param>
        // <DebuggerStepThrough()> _
        public void MsgErr(string Text)
        {
            // If geLanguage = EnumLanguage.Vietnamese Then
            // MessageBox.Show(Text, "Læi", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            // Else
            // MessageBox.Show(Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            // End If
            D99C0008.Msg(ConvertVietwareFToUnicode(Text), L3Resource.rL3("Loi"), L3MessageBoxButtons.OK, L3MessageBoxIcon.Err);
        }

        public string ConvertVietwareFToUnicode(string sText)
        {
            string sRet = sText;

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(249)), "ỳ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(252)), "ý");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(255)), "ỵ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(250)), "ỷ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(251)), "ỹ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(238)), "ù");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(242)), "ú");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(243)), "ụ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(239)), "ủ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(241)), "ũ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(167)), "ư");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(244)), "ừ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(247)), "ứ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(248)), "ự");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(245)), "ử");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(246)), "ữ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(223)), "ò");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(226)), "ó");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(227)), "ọ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(224)), "ỏ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(225)), "õ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(228)), "ồ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(231)), "ố");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(232)), "ộ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(229)), "ổ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(230)), "ỗ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(233)), "ờ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(236)), "ớ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(237)), "ợ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(234)), "ở");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(235)), "ỡ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(216)), "ì");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(219)), "í");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(220)), "ị");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(217)), "ỉ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(218)), "ĩ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(164)), "ô");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(165)), "ơ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(170)), "à");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(192)), "á");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(193)), "ạ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(182)), "ả");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(186)), "ã");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(376)), "ă");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(194)), "ằ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(197)), "ắ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(198)), "ặ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(195)), "ẳ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(196)), "ẵ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(161)), "â");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(199)), "ầ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(202)), "ấ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(203)), "ậ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(200)), "ẩ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(201)), "ẫ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(204)), "è");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(207)), "é");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(209)), "ẹ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(205)), "ẻ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(206)), "ẽ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(163)), "ê");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(210)), "ề");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(213)), "ế");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(214)), "ệ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(211)), "ể");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(212)), "ễ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(162)), "đ");

            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(8211)), "Ă");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(8212)), "Â");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(732)), "Đ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(8482)), "Ê");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(353)), "Ô");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(8250)), "Ơ");
            sRet = sRet.Replace(System.Convert.ToString(Strings.ChrW(339)), "Ư");

            return sRet;
        }

        public string CreateIGEVoucherNoNew(L3LookUpEdit c1Combo, string sVoucherTableName, string sVoucherIGE)
        {
            return CreateIGEVoucherNoNew(sVoucherTableName,
                sVoucherIGE,
                L3ConvertType.L3Int(c1Combo.ReturnValue("S1Type")),
                c1Combo.ReturnValue("S1"),
                L3ConvertType.L3Int(c1Combo.ReturnValue("S2Type")),
                c1Combo.ReturnValue("S2"),
                L3ConvertType.L3Int(c1Combo.ReturnValue("S3Type")),
                c1Combo.ReturnValue("S3"),
                L3ConvertType.L3Int(c1Combo.ReturnValue("OutputOrder")),
                L3ConvertType.L3Int(c1Combo.ReturnValue("OutputLength")),
                c1Combo.ReturnValue("Separator"), ref c1Combo);
        }

        public string CreateIGEVoucherNoNew(string sVoucherTableName, string sVoucherIGE, int S1Type, string s1, int S2Type, string S2, int S3Type, string S3, int OutputOrder, int OutputLength, string Seperator, ref L3LookUpEdit c1Combo)
        {

            // *********************************
            // Kiểm tra IGE của khóa chính phải có
            if (sVoucherIGE == "")
            {
                D99C0008.MsgL3("Do vấn đề về kỹ thuật (Khóa chính chưa được tạo) nên việc tạo phiếu bị lỗi." + Environment.NewLine + "Chương trình kết thúc.", L3MessageBoxIcon.Err);
                Lemon3.IO.L3LogFile.WriteLogFile("Loi Sinh so phieu (tao phieu) cua table TableName " + sVoucherTableName, "LogCreateIGEVoucherNoNew.log");
                Application.Exit();
            }
            // *********************************

            string strS1 = "";
            string strS2 = "";
            string strS3 = "";

            if (!Information.IsDBNull(S1Type) && S1Type != 0)
                strS1 = FindSxType(S1Type.ToString(), s1.Trim());
            if (!Information.IsDBNull(S2Type) && S2Type != 0)
                strS2 = FindSxType(S2Type.ToString(), S2.Trim());
            if (!Information.IsDBNull(S3Type) && S3Type != 0)
                strS3 = FindSxType(S3Type.ToString(), S3.Trim());

            string sSQL = "";
            sSQL = SQLStoreD91P9111(sVoucherIGE, sVoucherTableName, strS1.Trim(), strS2.Trim(), strS3.Trim(), OutputLength, OutputOrder, Seperator.Trim());

            DataTable dt;
            dt = L3SQLServer.ReturnDataTable(sSQL);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    // Update 13/03/2015: gán Lastkey cho Combo
                    if (c1Combo != null)
                        c1Combo.Tag = L3ConvertType.L3String(dt.Rows[0]["Lastkey"].ToString());
                }
                catch (Exception ex)
                {
                }
                return dt.Rows[0]["VoucherNo"].ToString();
            }
            else
            {
                D99C0008.MsgL3("Do vấn đề về kỹ thuật (Khóa chính chưa được tạo) nên việc tạo phiếu bị lỗi." + Environment.NewLine + "Chương trình kết thúc.", L3MessageBoxIcon.Err);
                Lemon3.IO.L3LogFile.WriteLogFile("Loi Sinh so phieu (tao phieu) cua store D91P9111 " + sSQL, "LogCreateIGEVoucherNoNew.log");

                Application.Exit();
            }
            return "";
        }

        public string SQLStoreD91P9111(string VoucherIGE, string VoucherTableName, string S1, string S2, string S3, int OutputLength, int OutputOrder, string Seperator)
        {
            string sSQL = "----Sinh so phieu tu dong theo dang moi va kiem tra trung phieu tu dong tang" + Environment.NewLine;
            sSQL += SQLStoreD91P9111(VoucherIGE, VoucherTableName, S1, S2, S3, OutputLength, OutputOrder, Seperator, "", 0, 0, 0, 0);
            return sSQL;
        }

        public string SQLStoreD91P9111(string VoucherIGE, string VoucherTableName, string S1, string S2, string S3, int OutputLength, int OutputOrder, string Seperator, string KeyString, byte IsGetLastKey, byte IsSaveNewLastKey, long LastKeyNew, byte IsRefeshLastKey)
        {
            string sSQL = "----Sinh so phieu tu dong theo dang moi va kiem tra trung phieu tu dong tang" + Environment.NewLine;
            sSQL += SQLStoreD91P9111(VoucherIGE, VoucherTableName, S1, S2, S3, OutputLength, OutputOrder, Seperator, "", 0, 0, 0, 0, "D91T0001");
            return sSQL;
        }

        public bool CheckDuplicateVoucherNoNew(string ModuleID, string TableName, string VoucherID, string VoucherNo)
        {
            // *********************************
            // Update 17/03/2015: kiểm tra trùng phiếu không cần dạng này
            // Kiểm tra IGE của khóa chính phải có
            // If VoucherID = "" Then
            // '            D99C0008.MsgL3("Khóa chính của nghiệp vụ này chưa được tạo", L3MessageBoxIcon.Exclamation)
            // '            Return True
            // D99C0008.MsgL3("Do vấn đề về kỹ thuật (Khóa chính chưa được tạo) nên việc kiểm tra trùng phiếu bị lỗi." & vbCrLf & "Chương trình kết thúc.", L3MessageBoxIcon.Err)
            // WriteLogFile("Loi Sinh so phieu (kiem tra trung phieu) cua table TableName " & TableName, "LogCreateIGEVoucherNoNew.log")
            //+ L3.COMMA
            // 
            // End If
            // *********************************
            string sSQL = "";
            sSQL = SQLStoreD91P9114(ModuleID.Substring(1, 2), TableName, VoucherID, VoucherNo);
            DataTable dt = new DataTable();
            dt = L3SQLServer.ReturnDataTable(sSQL);
            if (dt.Rows.Count > 0)
            {
                switch (System.Convert.ToInt32(dt.Rows[0]["Status"]))
                {
                    case 1:
                        {
                            // MessageBox.Show(dt.Rows(0).Item("Message").ToString, MsgAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            D99C0008.MsgL3(ConvertVietwareFToUnicode(dt.Rows[0]["Message"].ToString()), L3MessageBoxIcon.Exclamation);
                            dt = null/* TODO Change to default(_) if this is not a reference type */;
                            return true;
                        }
                }
            }
            dt = null/* TODO Change to default(_) if this is not a reference type */;
            return false;
        }

        private string SQLStoreD91P9114(string ModuleID, string TableName, string VoucherID, string VoucherNo)
        {
            string sSQL = "---- Kiem tra trung phieu " + Constants.vbCrLf;
            sSQL += "Exec D91P9114 ";
            sSQL += L3SQLClient.SQLString(L3.DivisionID) + L3.COMMA; // DivisionID, VarChar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(ModuleID) + L3.COMMA; // ModuleID, VarChar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(TableName) + L3.COMMA; // TableName, VarChar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(VoucherID) + L3.COMMA; // VoucherID, VarChar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(VoucherNo) + L3.COMMA; // VoucherNo, VarChar[20], NOT NULL
            sSQL += L3SQLClient.SQLString(L3.STRLanguage) + L3.COMMA; // Language, varchar[50], NOT NULL
            sSQL += L3SQLClient.SQLNumber(L3.TranYear); // TranYear, int, NOT NULL
            return sSQL;
        }


        public void InsertVoucherNoD91T9111(string sVoucherNo, string sVoucherTableName, string sVoucherIGE)
        {
            string sSQL = "";
            // sSQL = "INSERT INTO D91T9111 (VoucherNo, VoucherIGE, VoucherTableName)" & vbCrLf
            // sSQL &= "SELECT " & SQLString(sVoucherNo) & COMMA
            // sSQL &= SQLString(sVoucherIGE) & COMMA
            // sSQL &= SQLString(sVoucherTableName) & vbCrLf
            // sSQL &= "WHERE NOT EXISTS (" & vbCrLf
            // sSQL &= "SELECT TOP 1 1 "
            // sSQL &= "FROM D91T9111 WITH(NOLOCK) "
            // sSQL &= "WHERE	VoucherNo = " & SQLString(sVoucherNo)
            // sSQL &= ")"

            // Update 05/03/2015
            sSQL += SQLStoreD91P9113(sVoucherNo, sVoucherTableName, "", 1, sVoucherIGE);
            ExecuteSQLNoTransaction(sSQL);
        }

        private string SQLStoreD91P9113(string sVoucherNo, string sVoucherTableName, string FieldNameVoucherNo = "", byte iIsInsertNoAuto = 0, string sVoucherIGE = "")
        {
            string sSQL = "";
            if (iIsInsertNoAuto == 1)
                sSQL += ("-- Insert so phieu khong tao tu dong vao bang D91T9111" + Environment.NewLine);
            else
                sSQL += ("-- Xoa so phieu bang D91T9111" + Environment.NewLine);
            sSQL += "Exec D91P9113 ";
            sSQL += L3SQLClient.SQLString(sVoucherTableName) + L3.COMMA; // VoucherTableName, varchar[50], NOT NULL
            sSQL += "N" + L3SQLClient.SQLString(sVoucherNo) + L3.COMMA; // VoucherNo, varchar[50], NOT NULL
            sSQL += L3SQLClient.SQLString(L3.DivisionID) + L3.COMMA; // DivisionID, varchar[50], NOT NULL
            sSQL += L3SQLClient.SQLNumber(L3.TranYear) + L3.COMMA; // TranYear, int, NOT NULL
            sSQL += L3SQLClient.SQLString(FieldNameVoucherNo) + L3.COMMA;  // FieldVoucherNo, varchar[50], NOT NULL
            sSQL += L3SQLClient.SQLNumber(iIsInsertNoAuto) + L3.COMMA; // IsInsertNoAuto, int, NOT NULL
            sSQL += L3SQLClient.SQLString(sVoucherIGE); // VoucherIGE, varchar[50], NOT NULL
            return sSQL;
        }


        public void DeleteVoucherNoD91T9111_Transaction(string sVoucherNo, string sVoucherTableName, string FieldNameVoucherNo, L3LookUpEdit tdbcVoucherTypeID, bool bEditVoucherNo)
        {
            // Xóa số phiếu
            DeleteVoucherNoD91T9111(sVoucherNo, sVoucherTableName, FieldNameVoucherNo);

            // Cờ bEditVoucherNo: = True có sửa Số phiếu theo F2, = False: Không sửa số phiếu theo F2
            // Nếu là số phiếu sinh tự động và không nhấn F2 sửa số phiếu
            if (tdbcVoucherTypeID.ReturnValue("Auto") == "1" & bEditVoucherNo == false)
            {
                string sKeyString = "";
                long nOldLastKey;
                string sCharacter = ""; // tdbcVoucherTypeID.Columns("Separator").Text.Trim

                string sS1 = "";
                string sS2 = "";
                string sS3 = "";
                if (tdbcVoucherTypeID.ReturnValue("S1Type") != "0")
                    sS1 = FindSxType(tdbcVoucherTypeID.ReturnValue("S1Type"), tdbcVoucherTypeID.ReturnValue("S1").Trim());
                if (tdbcVoucherTypeID.ReturnValue("S2Type") != "0")
                    sS2 = FindSxType(tdbcVoucherTypeID.ReturnValue("S2Type"), tdbcVoucherTypeID.ReturnValue("S2").Trim());
                if (tdbcVoucherTypeID.ReturnValue("S3Type") != "0")
                    sS3 = FindSxType(tdbcVoucherTypeID.ReturnValue("S3Type"), tdbcVoucherTypeID.ReturnValue("S3").Trim());

                OutOrderEnum enOutOrderEnum;
                Enum.TryParse(tdbcVoucherTypeID.ReturnValue("OutputOrder"), out enOutOrderEnum);

                switch (enOutOrderEnum)
                {
                    case OutOrderEnum.lmSSSN:
                        {
                            sKeyString = ConcatenateKeys(sS1, sS2, sS3, "", sCharacter);
                            break;
                        }

                    case OutOrderEnum.lmSSNS:
                        {
                            sKeyString = ConcatenateKeys(sS1, sS2, "", sS3, sCharacter);
                            break;
                        }

                    case OutOrderEnum.lmSNSS:
                        {
                            sKeyString = ConcatenateKeys(sS1, "", sS2, sS3, sCharacter);
                            break;
                        }

                    case OutOrderEnum.lmNSSS:
                        {
                            sKeyString = ConcatenateKeys("", sS1, sS2, sS3, sCharacter);
                            break;
                        }
                }

                if (tdbcVoucherTypeID.Tag != "")
                    nOldLastKey = L3ConvertType.L3Int(tdbcVoucherTypeID.Tag);
                else
                    // Select vào bảng D91T0000 để lấy LastKey của SỐ phiếu
                    nOldLastKey = GetLastKey(sKeyString, "D91T0001") - 1;// Hàm GetLastKey trả về giá trị LastKey đã +1 so với Server

                // Update lại Last key khi Lưu tại nghiệp vụ bị lỗi
                if (nOldLastKey <= 0)
                    nOldLastKey = 0;
                else
                    nOldLastKey -= 1;
                SaveLastKey("D91T0001", sKeyString, nOldLastKey, 1);
            }
        }
        public void SaveLastKey(string sTableName, string sKeyString, long nLastKey, int iIsRefeshLastKey)
        {
            SaveLastKey(sTableName, sKeyString, nLastKey, iIsRefeshLastKey, "");
        }


        public void DeleteVoucherNoD91T9111(string sVoucherNo, string sVoucherTableName, string FieldNameVoucherNo)
        {
            string sSQL = "";
            // sSQL &= "IF NOT EXISTS (SELECT TOP 1 1 FROM " & sVoucherTableName & " WITH(NOLOCK) WHERE " & FieldNameVoucherNo & " = " & SQLString(sVoucherNo) & ")" & vbCrLf
            sSQL += SQLStoreD91P9113(sVoucherNo, sVoucherTableName, FieldNameVoucherNo);
            ExecuteSQLNoTransaction(sSQL);
        }


    }
}
