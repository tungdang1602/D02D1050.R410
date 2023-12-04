using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using Lemon3;
using Lemon3.Controls.DevExp;
using Lemon3.Controls.DevExp.Layout;
using Lemon3.Data;
using Lemon3.DataFinan;
using Lemon3.Functions;
using Lemon3.IO;
using Lemon3.LoadFN;
using Lemon3.Reports;
using Lemon3.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace D02D1050
{
    /// <summary>
    /// Interaction logic for D02F2080.xaml
    /// </summary>
    public partial class D02F2080 : L3Page
    {
        public D02F2080()
        {
            InitializeComponent();
        }
        public override void SetContentForL3Page()
        {

        }
        private EnumFormState _formState = EnumFormState.FormView;
        private void L3Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            _formState = EnumFormState.FormView;
            L3SimpleButton.SetImage(btnSave, btnNotSave);
            btnFilter.SetImage(ImageType.Filter);
            L3Control.SetShortcutPopupMenu(MainMenuControl01, ContextMenu1, ContextMenu2);
            L3Format.LoadCustomFormat();
            LoadLanguage();
            LoadFormatAndDefault();
            LoadTDBCombo();
            LoadTDBDropdown();
            EnableMenu(false);
            ResetFormView();
            grpDetail.IsEnabled = false;
            this.Cursor = Cursors.Arrow;
        }
        private void LoadLanguage()
        {
            this.Title = L3Resource.rL3("Ke_hoach_kiem_ke") + " - " + this.GetType().Name; // Kế hoạch kiểm kê

            // GroupBox
            grpInfo.Caption = L3Resource.rL3("Thong_tin_chung"); // Thông tin chung
            grpDetail.Caption = L3Resource.rL3("Thong_tin_chi_tiet"); // Thông tin chi tiết

            // Label
            lblPlanCodeD.Content = L3Resource.rL3("Ma_ke_hoach"); // Mã kế hoạch
            lblApprovalStatus.Content = L3Resource.rL3("Trang_thai_duyet"); // Trạng thái duyệt
            lblMethodID.Content = L3Resource.rL3("PP_tao_ma"); // PP tạo mã
            lblPlanCodeM.Content = L3Resource.rL3("Ma_ke_hoach"); // Mã kế hoạch
            lblStatusID.Content = L3Resource.rL3("Trang_thai_thuc_hien"); // Trạng thái thực hiện
            lblApprovalID.Content = L3Resource.rL3("Trang_thai_duyet"); // Trạng thái duyệt
            lblDate.Content = L3Resource.rL3("Thoi_gian_thuc_hien"); // Thời gian thực hiện
            lblDescription.Content = L3Resource.rL3("Dien_giai"); // Diễn giải
            // Combobox
            tdbcApprovalStatus.SetCaptionColumn("ApprovalID", L3Resource.rL3("Ma"));
            tdbcApprovalStatus.SetCaptionColumn("ApprovalStatus", L3Resource.rL3("Ten"));
            tdbcMethodID.SetCaptionColumn("IGEMethodID", L3Resource.rL3("Ma"));
            tdbcMethodID.SetCaptionColumn("IGEMethodName", L3Resource.rL3("Ten"));
            tdbcStatusID.SetCaptionColumn("StatusID", L3Resource.rL3("Ma"));
            tdbcStatusID.SetCaptionColumn("StatusName", L3Resource.rL3("Ten"));
            //dateDateTo.SetCaptionColumn("Period", L3Resource.rL3("Thang"));
            //dateDateFrom.SetCaptionColumn("Period", L3Resource.rL3("Thang"));

            // GridColumn
            COLM_PlanCode.Header = L3Resource.rL3("Ma_ke_hoach"); // Mã kế hoạch
            COLM_StatusName.Header = L3Resource.rL3("Trang_thai_thuc_hien"); // Trạng thái thực hiện
            COLM_ApprovalStatus.Header = L3Resource.rL3("Trang_thai_duyet"); // Trạng thái duyệt
            COLM_ExecutionTime.Header = L3Resource.rL3("Thoi_gian_thuc_hien"); // Thời gian thực hiện
            COLD_Num.Header = L3Resource.rL3("STT"); // STT
            COLD_AssetID.Header = L3Resource.rL3("Ma_tai_san"); // Mã tài sản
            COLD_AssetName.Header = L3Resource.rL3("Ten_tai_san"); // Tên tài sản
            COLD_ObjectID.Header = L3Resource.rL3("Bo_phan_tiep_nhan"); // Bộ phận quản lý
            COLD_NotesU.Header = L3Resource.rL3("Ghi_chu"); // Ghi chú
            COLD_ManagementObjID.Header = L3Resource.rL3("Bo_phan_quan_ly");
            // Button
            btnFilter.Content = L3Resource.rL3("Loc"); // Lọc
            btnSave.Content = L3Resource.rL3("Luu_CRM"); // Lưu
            btnNotSave.Content = L3Resource.rL3("CRM_Khong_luu"); // Không lưu
        }
        private DataTable dtPeriod = null;
        private void LoadTDBCombo()
        {
            string sSQL = "";
            //Combo Thời gian thực hiện
            //dtPeriod = L3SQLServer.ReturnDataTable(SQLSelectD01T9999().ToString());
            //L3DataSource.LoadDataSource(tdbcPeriodFrom, dtPeriod.Copy());
            //L3DataSource.LoadDataSource(tdbcPeriodTo, dtPeriod.Copy());
            //tdbcPeriodFrom.SelectedIndex = 0;
            //tdbcPeriodTo.SelectedIndex = 0;

            //combo trạng thái thực hiện
            sSQL = "--Load cbo trạng thái thực hiện" + Environment.NewLine;
            sSQL += "SELECT 		CONVERT(tinyint, 0) AS StatusID , N'Đang nhập liệu' AS StatusName  " + Environment.NewLine;
            sSQL += "UNION ALL  " + Environment.NewLine;
            sSQL += "SELECT 		CONVERT(tinyint, 1) AS StatusID , N'Hoàn tất nhập liệu' AS StatusName  ";
            L3DataSource.LoadDataSource(tdbcStatusID, sSQL, L3.IsUniCode);

            //combo trạng thái duyệt (Group Thông tin chung)
            sSQL = "--Load cbo Trạng thái duyệt" + Environment.NewLine;
            sSQL += "SELECT 		'0' AS IsApproved, N'Chưa duyệt' AS ApprovalStatus " + Environment.NewLine;
            sSQL += "UNION ALL " + Environment.NewLine;
            sSQL += "SELECT 		'90' AS IsApproved, N'Đã duyệt' AS ApprovalStatus " + Environment.NewLine;
            sSQL += " UNION ALL " + Environment.NewLine;
            sSQL += "SELECT 		 '100' AS IsApproved, N'Từ chối' AS ApprovalStatus";
            L3DataSource.LoadDataSource(tdbcApprovalStatus, sSQL, L3.IsUniCode);

            // PP tạo mã
            LoadtdbcIGEMethodID(tdbcMethodID, "02", "D02F0080");
            //Lemon3.DataFinan.IGEMethodID.LoadtdbcIGEMethodID(tdbcMethodID, "02", "D02F0080", L3ConvertType.L3String(L3.DivisionID)));
            //Lemon3.LoadFN.L3IGEMethodID.LoadtdbcIGEMethodID(tdbcMethodID,"02", "D02F0080", );
            //Lemon3.DataFinan.IGEMethodID.LoadtdbcIGEMethodID(tdbcMethodID, "02", "D02F0080", L3SQLClient.SQLString(L3.DivisionID), "");
            //DataTable dtVoucher = Lemon3.LoadFN.L3IGEMethodID.ReturnTableIGEMethodID("02", "D02F0080", L3ConvertType.L3String(L3.DivisionID), "MethodID", "MethodName");
            //L3DataSource.LoadDataSource(tdbcMethodID, dtVoucher);
            //DataRow[] dr = dtVoucher.Select("Defaults =1");
            //if (dr.Length == 0)
            //    return;
            //tdbcMethod.EditValue = dr[0]["IGEMethodID"];
        }

        private void LoadTDBDropdown()
        {
            string sSQL = "";
            //Dropdown Mã tài sản
            sSQL = "--Load dropdown ma tai san" + Environment.NewLine;
            sSQL += SQLStoreD02P2080("Đổ nguồn dd tài sản","DD_Asset");
            L3DataSource.LoadDataSource(tdbdAssetID, sSQL, L3.IsUniCode);

        }
        public DataTable ReturnTableIGEMethodID(string sModuleID, string sFormID, string sDivisionID = "", string sEditIGEMethodID = "")
        {
            if (sDivisionID == "")
                sDivisionID = L3.DivisionID;
            string sSQL = "--Do nguon PP tao phieu" + Environment.NewLine
                + "SELECT IGEMethodID , IGEMethodNameU As IGEMethodName, Defaults, FormulaU as Formula" + Environment.NewLine
                + "FROM D91T0045 WITH(NOLOCK)" + Environment.NewLine
                + "WHERE ModuleID = " + L3SQLClient.SQLString(sModuleID)
                + " And Disabled = 0 "
                + " And FormID = " + L3SQLClient.SQLString(sFormID) + Environment.NewLine
                + " And (DivisionID = " + L3SQLClient.SQLString(sDivisionID)
                + " Or DivisionID = '' )" + Environment.NewLine;
            if (sEditIGEMethodID != "")
                sSQL += " Or VoucherTypeID = " + L3SQLClient.SQLString(sEditIGEMethodID) + Environment.NewLine;
            sSQL += " ORDER BY 	IGEMethodID";
            return L3SQLServer.ReturnDataTable(sSQL);
        }
        private void LoadtdbcIGEMethodID(L3LookUpEdit tdbcIGEMethodID, string sModuleID, string sFormID, string sDivisionID = "", string sEditIGEMethodID = "")
        {
            try
            {
                DataTable dtVoucher = ReturnTableIGEMethodID(sModuleID, sFormID, sDivisionID);
                L3DataSource.LoadDataSource(tdbcIGEMethodID, dtVoucher, L3.IsUniCode);
                DataRow[] dr = dtVoucher.Select("Defaults =1");
                if (dr.Length == 0)
                    return;
                tdbcIGEMethodID.EditValue = dr[0]["IGEMethodID"];
            }
            catch (Exception ex)
            {
                Lemon3.Messages.L3Msg.MyMsg("LoadtdbcIGEMethodID() " + ex.Message);
                return;
            }
        }

        private void LoadFormatAndDefault()
        {
            L3Control.SetBackColorObligatory(tdbcMethodID, txtPlanCodeD, dateDateFrom, dateDateTo);
            L3GridControl.SetBackColorObligatory(COLD_AssetID);
            txtApprovalStatus.Background = Brushes.Silver;
            dateDateFrom.InputDate("MM/dd/yyyy");
            dateDateTo.InputDate("MM/dd/yyyy");

            dateDateFrom.Mask ="MM/yyyy";
            dateDateTo.Mask ="MM/yyyy";

            tdbgD.SetColumnOrderNum(COLD_Num);
            // Backup cột tĩnh
            _lsColsBeforeBackup.AddRange(new GridColumn[] { COLD_Num, COLD_AssetID, COLD_AssetName, COLD_ObjectID, COLD_ManagementObjID, COLD_NotesU });
        }

        //private bool CheckValidPeriodFromTo()
        //{
            
        //        int iFrom = 0;
        //        int iTo = 0;
        //        iFrom = L3ConvertType.L3Int(tdbcPeriodFrom.ReturnValue("TranYear")) * 100 + L3ConvertType.L3Int(tdbcPeriodFrom.ReturnValue("TranMonth"));
        //        iTo = L3ConvertType.L3Int(tdbcPeriodTo.ReturnValue("TranYear")) * 100 + L3ConvertType.L3Int(tdbcPeriodTo.ReturnValue("TranMonth"));
        //        if (iFrom > iTo)
        //        {
        //            D99D0041.D99C0008.MsgL3(L3Resource.rL3("MSG000013"));
        //            tdbcPeriodTo.Focus();
        //            return false;
        //        }
        //        return true;
            
        //}
        private bool CheckValidDateFromTo(L3DateEdit dateFrom, L3DateEdit dateTo, bool bObligatory = true)
        {
            // Chưa nhập giá trị Từ Đến
            if (bObligatory & dateFrom.Text == "" & dateTo.Text == "")
            {
                D99D0041.D99C0008.MsgNotYetChoose(Lemon3.Resources.L3Resource.rL3("Ngay"));
                dateFrom.Focus();
                return false;
            }
            else if (dateTo.Text == "")
                dateTo.EditValue = dateFrom.EditValue;
            else if (dateFrom.Text == "")
                dateFrom.EditValue = dateTo.EditValue;
            else if ((DateTime)dateFrom.EditValue > (DateTime)dateTo.EditValue)
            {
                D99D0041.D99C0008.MsgNotYetChoose(Lemon3.Resources.L3Resource.rL3("MSG000013"));
                dateTo.Focus();
                return false;
            }
            return true;
        }


        private void EnableMenu(bool bEnable)
        {
            try
            {
                if (!bEnable)
                    L3Control.CheckMenu(this.GetType().Name, MainMenuControl01, ContextMenu1, tdbgM.VisibleRowCount, true, false);
                else
                    L3Control.CheckMenu("-1", MainMenuControl01, ContextMenu1, -1, false, false);
            }
            catch (Exception ex)
            {
                D99D0041.D99C0008.MsgL3(ex.Message + Environment.NewLine + ex.StackTrace);
                return;
            }
        }
        private void EnableMenuDetail()
        {
            try 
            {
                if (_formState == EnumFormState.FormView && tdbgD.VisibleRowCount > 0)
                    mnsExportToExcel.IsEnabled = true;
                else
                    mnsExportToExcel.IsEnabled = false;
            }
            catch(Exception ex)
            {
                return;
            }
        }
        private DataRow dr91P1000 = null;
        private void tdbcMethodID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            try {
                //if (tdbcMethodID.ReturnValue() != "" && _formState == EnumFormState.FormAdd)
                //{
                //    IGEMethodID.MethodAuto[] sMethod = new IGEMethodID.MethodAuto[0];
                //    //txtPlanCodeD.EditValue = Lemon3.DataFinan.IGEMethodID.CreateIGEMethodID(tdbcMethodID.ReturnValue(), "02", "D02F0080", ref dr91P1000, sMethod);
                //    //txtPlanCodeD.EditValue = Lemon3.LoadFN.L3IGEMethodID.CreateIGEMethodID(tdbcMethodID.ReturnValue(), "02", "D02F0080", ref dr91P1000, sMethod);
                //    txtPlanCodeD.EditValue = Lemon3.LoadFN.L3IGEMethodID.CreateIGEMethodID(tdbcMethodID.ReturnValue(), "02", "D02F0080",ref dr91P1000);
                //    txtPlanCodeD.IsReadOnly = true;
                //}
                //else
                //    txtPlanCodeD.IsReadOnly = false;
                if (_formState == EnumFormState.FormView) return;
                if (e.NewValue != null && Convert.ToString(e.NewValue) != "")
                {
                    if (txtPlanCodeD != null) txtPlanCodeD.IsReadOnly = true;
                    IGEMethodID.MethodAuto[] sMethod = new IGEMethodID.MethodAuto[1];
                    txtPlanCodeD.EditValue = Lemon3.DataFinan.IGEMethodID.CreateIGEMethodID(L3ConvertType.L3String(tdbcMethodID.EditValue), "02", "D02F0080", ref dr91P1000, sMethod);
                }
                else
                {
                    if (txtPlanCodeD != null) txtPlanCodeD.IsReadOnly = false;
                    txtPlanCodeD.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                Lemon3.Messages.L3Msg.MyMsg("tdbcMethodID_EditValueChanged() " + ex.Message);
                return;
            }

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try {
                LoadTDBGridM();
                EnableMenu(false);
                if (tdbgM.VisibleRowCount != 0)
                    grpDetail.IsEnabled = true;
                else
                {
                    grpDetail.IsEnabled = false;
                    ClearTextALL();
                }

            }
            catch(Exception ex)
            {
                Lemon3.Messages.L3Msg.MyMsg("btnFilter_Click() " + ex.Message);
                return;
            }
        }
        private void LoadTDBGridM(bool bReFilter = false, string sKeyID = "")
        {
            string sSQL = "";
            sSQL = SQLStoreD02P2080("Đổ nguồn lưới master","LoadMaster","",txtPlanCodeM.Text, tdbcApprovalStatus.ReturnValue());
            _dtGridM = L3SQLServer.ReturnDataTable(sSQL);
            L3DataSource.LoadDataSource(tdbgM, _dtGridM, L3.IsUniCode);

            if (!string.IsNullOrEmpty(sKeyID))
            {
                if (!tdbgM.IsFocused)
                    tdbgM.Focus();
                for (int i = 0; i < tdbgM.VisibleRowCount; i++)
                {
                    DataRow drLoop = (tdbgM.GetRow(tdbgM.GetRowHandleByVisibleIndex(i)) as DataRowView).Row;
                    if (drLoop[COLM_BatchID.FieldName].ToString() == sKeyID)
                    {
                        HandleRowFocus(i);
                        break;
                    }
                }
            }
        }
        private void ResetGrid()
        {
            EnableMenu(false);
        }
        private void HandleRowFocus(int indexRowFocus)
        {
            try
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    tdbgMView.MoveFocusedRow(indexRowFocus);
                    tdbgMView.SelectRow(indexRowFocus);
                }), DispatcherPriority.Render);

                tdbgM.SelectedItem = indexRowFocus;
                tdbgMView.FocusedRowHandle = indexRowFocus;
            }
            catch (Exception ex)
            {
                D99D0041.D99C0008.MsgL3("HandleRowFocus() " + ex.Message);
                return;
            }
        }

       /// =============================== Lưới 2 ===============================
       DataTable _dtGridDBandColumnDetail = null, _dtGridD, _dtGridM;

       L3CreateColumnsForGridControl _oL3CreateColumnsDetail = null;
       List<GridColumn> _lsColsBeforeBackup = new List<GridColumn>(); // Backup cột design nằm trước cột động

       private void LoadEdit()
       {
           try 
           {
               if (_dtGridM.Rows.Count == 0) return;
               for (int i = 0; i < _dtGridM.Rows.Count; i++)
               {
                   if (_dtGridM.Rows[i]["BatchID"] == sTemp)
                   {
                       tdbcMethodID.EditValue = _dtGridM.Rows[i]["MethodID"];
                       txtPlanCodeD.EditValue = _dtGridM.Rows[i]["PlanCode"];
                       tdbcStatusID.EditValue = _dtGridM.Rows[i]["StatusID"];
                       txtApprovalStatus.EditValue = _dtGridM.Rows[i]["ApprovalStatus"];
                       txtBatchID.EditValue = _dtGridM.Rows[i]["BatchID"];
                       txtAStatusID.EditValue = _dtGridM.Rows[i]["AStatusID"];
                       //string sPeriodF = "", sPeriodTo = "";
                       //for(int j = 0; j < dtPeriod.Rows.Count; j++)
                       //{
                       //    if(dtPeriod.Rows[j]["Date"].ToString() == _dtGridM.Rows[i]["DateFrom"].ToString())
                       //        sPeriodF = dtPeriod.Rows[j]["Period"].ToString();
                       //    if (dtPeriod.Rows[j]["Date"].ToString() == _dtGridM.Rows[i]["DateTo"].ToString())
                       //        sPeriodTo = dtPeriod.Rows[j]["Period"].ToString();
                       //}
                       dateDateFrom.EditValue = _dtGridM.Rows[i]["DateFrom"];
                       dateDateTo.EditValue = _dtGridM.Rows[i]["DateTo"];
                       txtDescription.EditValue = _dtGridM.Rows[i]["Description"];
                   }
               }
               Lemon3.Controls.DevExp.L3Control.ReadOnlyControl(true, tdbcMethodID, txtPlanCodeD, tdbcStatusID, txtApprovalStatus, txtDescription);
               
               tdbgDView.AllowEditing = false;
               btnSave.IsEnabled = false;
               btnNotSave.IsEnabled = false;
           }
           catch(Exception ex)
           {
               return;
           }
       }
        private void ResetFormView()
       {
           _formState = EnumFormState.FormView;
           tdbgDView.AllowEditing = false;
           EnableMenu(false);
           tdbgDView.NewItemRowPosition = NewItemRowPosition.None;
           Lemon3.Controls.DevExp.L3Control.ReadOnlyControl(true, tdbcMethodID, txtPlanCodeD, tdbcStatusID, dateDateTo, dateDateFrom, txtDescription);
           btnSave.IsEnabled = false;
           btnNotSave.IsEnabled = false;
           tdbgD.IsAllowDelete = false;
       }
        private void LoadAddNew()
       {
           try {
               _formState = EnumFormState.FormAdd;
               ClearTextALL();
               tdbcStatusID.SelectedIndex = 0;
               
               btnSave.IsEnabled = true;
               btnNotSave.IsEnabled = true;
               tdbgDView.AllowEditing = true;
               tdbgD.IsAllowDelete = true;
               Lemon3.Controls.DevExp.L3Control.ReadOnlyControl(false, tdbcMethodID, txtPlanCodeD, tdbcStatusID, dateDateFrom, dateDateTo, txtDescription);
           }
            catch(Exception ex)
           {
               return;
            }
       }
        private void ClearTextALL()
        {
            L3Control.ClearText(new DependencyObject[] { tdbcMethodID, txtPlanCodeD, tdbcStatusID, txtApprovalStatus, dateDateTo, dateDateFrom, txtDescription});
            if (_dtGridD != null) _dtGridD.Clear();
          
        }
       private void LoadTDBGridD()
       {
           tdbgD.ItemsSource = null;
           if (_formState == EnumFormState.FormAdd)
           {
               _dtGridDBandColumnDetail = L3SQLServer.ReturnDataTable(SQLStoreD02P2080("Dựng cột", "AddCol", "", "", "",L3ConvertType.L3String(Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy")),L3ConvertType.L3String(Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")), "", ""));
               _dtGridD = L3SQLServer.ReturnDataTable(SQLStoreD02P2080("Đổ nguồn cho lưới, cột động", "Loaddetail", "", "", "", Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy"), Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")));
           }
           else
           {
               _dtGridDBandColumnDetail = L3SQLServer.ReturnDataTable(SQLStoreD02P2080("Dựng cột", "AddCol", L3ConvertType.L3String(tdbgM.GetFocusedRowCellValue(COLM_BatchID)), "", "", Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy"), Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")));
               _dtGridD = L3SQLServer.ReturnDataTable(SQLStoreD02P2080("Đổ nguồn cho lưới, cột động", "Loaddetail", L3ConvertType.L3String(tdbgM.GetFocusedRowCellValue(COLM_BatchID)), "", "", Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy"), Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")));
           }
           

           if (_oL3CreateColumnsDetail == null) _oL3CreateColumnsDetail = new L3CreateColumnsForGridControl();
           _oL3CreateColumnsDetail.TDBGridRemoveAllBandAndColumn(tdbgD);
           // Chèn cột cứng trước cột động
           GridControlBand bandStaticBefore = new GridControlBand();
           bandStaticBefore.HorizontalHeaderContentAlignment = System.Windows.HorizontalAlignment.Center;
           //bandStaticBefore.Fixed = FixedStyle.Left;
           foreach (GridColumn gCol in _lsColsBeforeBackup)
           {
               bandStaticBefore.Columns.Add(gCol);
           }
           tdbgD.Bands.Add(bandStaticBefore);
           _oL3CreateColumnsDetail.CreateColumns(tdbgD, _dtGridDBandColumnDetail);
           L3DataSource.LoadDataSource(tdbgD, _dtGridD, L3.IsUniCode);

          
       }

       private void tdbgD_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
       {
           try
           { 
               if (e.Row == null) return;
               String FieldName = e.Column.FieldName;
               if (FieldName == COLD_AssetID.FieldName) // Dự án
               {
                   if (L3ConvertType.L3String(tdbgD.GetFocusedRowCellValue(FieldName)) == "")
                   {
                       tdbgD.SetCellValueRowFocused(COLD_AssetName, "");
                       tdbgD.SetCellValueRowFocused(COLD_ManagementObjID, "");
                       tdbgD.SetCellValueRowFocused(COLD_ObjectID, "");
                   }
                   else
                   {
                       tdbgD.SetCellValueRowFocused(COLD_AssetName, tdbdAssetID.ReturnValue("AssetName"));
                       tdbgD.SetCellValueRowFocused(COLD_ManagementObjID, tdbdAssetID.ReturnValue("ManagementObjID"));
                       tdbgD.SetCellValueRowFocused(COLD_ObjectID, tdbdAssetID.ReturnValue("ObjectID"));
                   }
               }
           }
           catch(Exception ex)
           {
               Lemon3.Messages.L3Msg.MyMsg("tdbgD_CellValueChanged() " + ex.Message);
               return;
           }
       }
       private string sTemp = "";
       private void tdbgMView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
       {
           try
           {
               if (tdbgM.VisibleRowCount == 0) return;
               sTemp = L3ConvertType.L3String(tdbgM.GetFocusedRowCellValue(COLM_BatchID));
               LoadEdit();
               LoadTDBGridD();
               EnableMenuDetail();
           }
           catch(Exception ex)
           {
               Lemon3.Messages.L3Msg.MyMsg("tdbgMView_FocusedRowChanged() " + ex.Message);
               return;
           }
       }
       private void tdbgDView_InitNewRow(object sender, InitNewRowEventArgs e)
       {
           for (int i = 0; i < _dtGridDBandColumnDetail.Rows.Count; i++)
           {
               string sFieldName;
               if (_dtGridDBandColumnDetail.Rows[i]["DataType"].ToString() == "C")
               {
                   sFieldName = _dtGridDBandColumnDetail.Rows[i]["FieldName"].ToString();
                   tdbgD.SetFocusedRowCellValue(sFieldName, false);
               }
           }

       }
       private bool AllowEdit()
       {
           try
           {
               if (!L3SQLServer.CheckStore(SQLStoreD02P2080("Kiểm tra khi sửa", "CheckEdit", L3ConvertType.L3String(tdbgM.GetFocusedRowCellValue(COLM_BatchID))))) return false;
               return true;
           }
           catch(Exception ex)
           {
               return false;
           }
           
       }

       private void tsbAdd_ItemClick(object sender, ItemClickEventArgs e)
       {
           grpDetail.IsEnabled = true;
           LoadAddNew();
           EnableMenu(true);
           EnableMenuDetail();
       }
       private void tsbEdit_ItemClick(object sender, ItemClickEventArgs e)
       {
           if (!AllowEdit()) return;
           _formState = EnumFormState.FormEdit;
           Lemon3.Controls.DevExp.L3Control.ReadOnlyControl(false, dateDateTo, dateDateFrom, tdbcStatusID, txtDescription);
           EnableMenu(true);
           tdbgDView.AllowEditing = true;
           btnSave.IsEnabled = true;
           btnNotSave.IsEnabled = true;
           EnableMenuDetail();
           tdbgDView.NewItemRowPosition = NewItemRowPosition.Bottom;
           tdbgD.IsAllowDelete = true;

       }
       private void tsbDelete_ItemClick(object sender, ItemClickEventArgs e)
       {
           if (Lemon3.Messages.L3Msg.AskDelete() == System.Windows.Forms.DialogResult.No) return;
           string sSQL = SQLStoreD02P2080("Kiểm tra khi xoá", "Delete", L3ConvertType.L3String(tdbgM.GetFocusedRowCellValue(COLM_BatchID)));
           bool bRunSQL = L3SQLServer.CheckStore(sSQL);
           if (bRunSQL)
           {
               Lemon3.Messages.L3Msg.DeleteOK();
               tdbgM.DeleteRowFocusEvent();
               // Nếu xoá hết các dòng lưới trái thì sẽ clear dữ liệu trên các control
               if (tdbgM.VisibleRowCount <= 0)
               {
                   ClearTextALL();
                   EnableMenu(false);
                   return;
               }
           }
           else
           {
               return;
           }
       }
       private void tsbPrint_ItemClick(object sender, ItemClickEventArgs e)
       {
           Print();
       }
       private void mnsAdd_Click(object sender, RoutedEventArgs e)
       {
           tsbAdd_ItemClick(null, null);
       }
       private void mnsEdit_Click(object sender, RoutedEventArgs e)
       {
           tsbEdit_ItemClick(null, null);
       }
       private void mnsDelete_Click(object sender, RoutedEventArgs e)
       {
           tsbDelete_ItemClick(null,null);
       }
       private void mnsPrint_Click(object sender, RoutedEventArgs e)
       {
           tsbPrint_ItemClick(null, null);
       }
       private void tdbgMView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
       {
           if (mnsEdit.IsEnabled == true)
               tsbEdit_ItemClick(null, null);
       }
       private void mnsExportToExcel_Click(object sender, RoutedEventArgs e)
       {
           {
               try
               {
                   bool bCheck = false;
                   if (tdbgD.Columns["OrderNumABCXYZ"] != null)
                   {
                       tdbgD.Columns["OrderNumABCXYZ"].FieldName = "Num";
                       bCheck = true;
                   }
                   for (int i = 0; i <= tdbgD.VisibleRowCount - 1; i++)
                   {
                       tdbgD.SetCellValue(i, COLD_Num, i + 1);
                   }
                   if (!L3ExportFromGrid.ExportToExcelFromGrid(tdbgD, "D02F2080_data.xls"))
                   {
                       this.Cursor = Cursors.Arrow;
                       if (bCheck) tdbgD.Columns["Num"].FieldName = "OrderNumABCXYZ";
                       return;
                   }
                   if (bCheck)
                   {
                       tdbgD.Columns["Num"].FieldName = "OrderNumABCXYZ";
                   }
               }
               catch (Exception ex)
               {
                   Lemon3.Messages.L3Msg.MyMsg("mnsExportToExcel_Click() " + ex.Message);
                   return;
               }
           }
       }
       private L3CrystalReport crystalReport;

       private void Print()
       {
           string sReportName = "";
           string sReportPath = "";
           string sReportTitle = "";
           string sReportTypeID = "D02F2080";
           string sFileExt = L3Report.GetReportPathNew("02", sReportTypeID, ref sReportName, "", ref sReportPath, ref sReportTitle);
           if (sReportName == "") return;
           string sSQL = SQLStoreD02P2080("IN", "Print", L3ConvertType.L3String(tdbgM.GetFocusedRowCellValue(COLM_BatchID)), "", "", L3ConvertType.L3String(Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy")), L3ConvertType.L3String(Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")));
           if (sFileExt.ToLower().Contains("rpt"))
           {
               crystalReport = new L3CrystalReport();
               crystalReport.AddSubD99R0000(L3.DivisionID);
               crystalReport.AddMain(sSQL);
               crystalReport.PrintReport(sReportPath, sReportTitle);
           }
           else if (sFileExt.ToLower().Contains("xls") || sFileExt.ToLower().Contains("xlsx"))
           {
               DataSet ds = L3SQLServer.ReturnDataSet(sSQL);
               DataTable[] arrDT = new DataTable[1];
               if (ds.Tables.Count >= 1)
               {
                   arrDT[0] = ds.Tables[0];
               }
               sReportPath = L3Report.GetObjectFile(sReportTypeID, sReportName, sFileExt, sReportPath);

               L3ReportExcel excelRe = new L3ReportExcel();
               excelRe.ExcelType = PrintExcelType.Normal;
               excelRe.MyExcel(sReportPath, sFileExt, false, arrDT);
               excelRe.OpenFileExcel(sReportPath);
           }
           else
           {
               D99D0041.D99C0008.MsgL3("Report format: ." + sFileExt + " not support");
           }
       }
       private void dateDateFrom_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
       {
           if (_formState == EnumFormState.FormAdd || _formState == EnumFormState.FormEdit)
               dateDateTo_EditValueChanged(null,null);
       }

       private void dateDateTo_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
       {
           if (dateDateTo.Text != "" && dateDateFrom.Text != "")
           {
               
               CheckValidDateFromTo(dateDateFrom, dateDateTo);
               LoadTDBGridD();
               if (_formState == EnumFormState.FormAdd)
                   tdbgDView.NewItemRowPosition = NewItemRowPosition.Bottom;
           }
       }
       //private void tdbcPeriodTo_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
       //{
          
       //        if (tdbcPeriodTo.ReturnValue() != "" && tdbcPeriodFrom.ReturnValue() != "")
       //        {
       //            CheckValidPeriodFromTo();
       //            LoadTDBGridD();
       //            if( _formState == EnumFormState.FormAdd)
       //                 tdbgDView.NewItemRowPosition = NewItemRowPosition.Bottom;
       //        }
               
           
       //}
       //private void tdbcPeriodFrom_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
       //{
       //    if (_formState == EnumFormState.FormAdd || _formState == EnumFormState.FormEdit)
       //            tdbcPeriodTo_EditValueChanged(null, null);
       //}
   
       #region SQL
       private StringBuilder SQLSelectD01T9999()
       {
           StringBuilder sSQL = new StringBuilder();
           sSQL.Append("-- Combo Ky" + Environment.NewLine);
           sSQL.Append("Select REPLACE(STR(TranMonth, 2), ' ', '0') +'/' + STR(TranYear, 4) AS Period, REPLACE(STR(TranMonth,2), ' ','0')+ '/01/' + STR(TranYear, 4) +' 00:00:00' as Date, " + Environment.NewLine);
           sSQL.Append("TranMonth, TranYear" + Environment.NewLine);
           sSQL.Append("From D01T9999 WITH(NOLOCK)" + Environment.NewLine);
           sSQL.Append("Where DivisionID = " + L3SQLClient.SQLString(L3.DivisionID) + Environment.NewLine);
           sSQL.Append("Order By TranYear DESC, TranMonth DESC");
           return sSQL;

       }

       private void btnSave_Click(object sender, RoutedEventArgs e)
       {
          btnSave.Focus();
           if (!btnSave.IsFocused)
               return;
           //Hỏi trước khi lưu
           if (Lemon3.Messages.L3Msg.AskSave() == System.Windows.Forms.DialogResult.No)
               return;
           SaveData(sender);
       }

       private void btnNotSave_Click(object sender, RoutedEventArgs e)
       {
           if (_formState == EnumFormState.FormAdd)
           {
               if (Lemon3.Messages.L3Msg.AskSave() == System.Windows.Forms.DialogResult.Yes)
                   return;
               ResetFormView();
               ClearTextALL();
           }
           else if (_formState == EnumFormState.FormEdit)
           {
               if (Lemon3.Messages.L3Msg.AskSave() == System.Windows.Forms.DialogResult.Yes)
                   return;
               LoadEdit();
               LoadTDBGridD();
               ResetFormView();
           }
       }
        private bool AllowSave()
       {
           if (!CheckValidDateFromTo(dateDateFrom, dateDateTo))
               return false;

           for (int i = 0; i < tdbgD.VisibleRowCount - 1; i++)
           {
               if (L3ConvertType.L3String(tdbgD.GetCellValue(i, COLD_AssetID)) == "")
               {
                   D99D0041.D99C0008.MsgNotYetEnter(COLD_AssetID.Header.ToString());
                   tdbgD.Focus();
                   tdbgDView.FocusedRowHandle = i;
                   tdbgDView.FocusedColumn = COLD_AssetID;
                   tdbgDView.ShowEditor(true);
                   return false;
               }
           }
           return true;
       }

        private string _batchID = "";
        string sOldVoucherNo = ""; // Lưu lại số phiếu cũ
        bool bEditVoucherNo = false; // = True: có nhấn F2; = False: không
        bool bFirstF2 = false; // Nhấn F2 lần đầu tiên
       private bool SaveData(object sender)
       {
           if (!AllowSave())
               return false;
           L3SQLBulkCopy bulkCopy = new L3SQLBulkCopy();
           L3CreateIGEVoucherNo L3IGEVoucherNo = new L3CreateIGEVoucherNo();

           switch(_formState)
           {
               case EnumFormState.FormAdd:
               StringBuilder sSQL = new StringBuilder();
                _batchID = L3CreateIGE.CreateIGE("D02T2080", "BatchID ", "02", "BA", L3CreateIGE.KeyString);
                //if (bEditVoucherNo == false)
                //{
                //   txtBatchID.EditValue = L3IGEVoucherNo.CreateIGEVoucherNoNew(tdbcMethodID, "D02T2080", _batchID);
                //}
                //else
                //{
                //    if (bEditVoucherNo == false)
                //    {
                //        // Kiểm tra trùng Số phiếu
                //        if (L3IGEVoucherNo.CheckDuplicateVoucherNoNew("D02", "D02T2080", L3ConvertType.L3String(_batchID), txtPlanCodeD.Text) == true)
                //        {
                //            btnSave.IsEnabled = true;
                //            btnNotSave.IsEnabled = true;
                //            this.Cursor = Cursors.Arrow;
                //            return false;
                //        }
                //    }
                //    else // Có nhấn F2
                //    {
                //        // Insert Số phiếu vào bảng D54T2220

                //    }
                //    // Insert VoucherNo vào bảng D91T9111
                //    L3IGEVoucherNo.InsertVoucherNoD91T9111(txtBatchID.Text, "D02T2080", _batchID);
                //}
                bEditVoucherNo = false;
                sOldVoucherNo = "";
                
                bulkCopy.AddSQLAfter(SQLStoreD02P2080("Lưu thêm mới", "SaveAddNew", L3ConvertType.L3String(_batchID), txtPlanCodeD.Text, "0", L3ConvertType.L3String(Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy")), L3ConvertType.L3String(Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")), L3ConvertType.L3String(tdbcMethodID.ReturnValue()), L3ConvertType.L3String(tdbcStatusID.ReturnValue()), txtDescription.Text ));
                
                   break;
               case EnumFormState.FormEdit:

                   bulkCopy.AddSQLAfter(SQLStoreD02P2080("Lưu khi sửa", "SaveEdit", txtBatchID.Text, txtPlanCodeD.Text, "0", L3ConvertType.L3String(Convert.ToDateTime(dateDateFrom.EditValue).ToString("dd/MM/yyyy")), L3ConvertType.L3String(Convert.ToDateTime(dateDateTo.EditValue).ToString("dd/MM/yyyy")), L3ConvertType.L3String(tdbcMethodID.ReturnValue()), L3ConvertType.L3String(tdbcStatusID.ReturnValue()), txtDescription.Text));

                   break;
           }
           this.Cursor = Cursors.Wait;
           bool bRunSQL = bulkCopy.SaveBulkCopy(_dtGridD.Copy(), "[#D02F2080_" + Environment.MachineName + "_" + L3.UserID + "]");
           this.Cursor = Cursors.Arrow;
           if (bRunSQL)
           {
               //_bSaved = true;
               Lemon3.Messages.L3Msg.SaveOK();
               if (_formState == EnumFormState.FormAdd)
               {
                   Lemon3.DataFinan.IGEMethodID.UpdateIGEMethodID("02","D02F0080", dr91P1000);
                   ResetFormView();
                   LoadTDBGridM(true, _batchID);
                   EnableMenu(false);
               }
               else if (_formState == EnumFormState.FormEdit)
               {
                   ResetFormView();
                   LoadTDBGridM(true, txtBatchID.Text);
                   EnableMenu(false);
               }
               
           }
           else
           {
               if (_formState == EnumFormState.FormAdd)
                   L3IGEVoucherNo.DeleteVoucherNoD91T9111_Transaction(_batchID, "D02T2080", "BatchID", tdbcMethodID, bEditVoucherNo);
               Lemon3.Messages.L3Msg.SaveNotOK();
               return false;
           }
           return true;
       }

       //#---------------------------------------------------------------------------------------------------
       //# Title: SQLStoreD02P2080
       //# Created User: Đặng Thanh Tùng
       //# Created Date: 03/02/2023 08:56:55
       //#---------------------------------------------------------------------------------------------------
       private String SQLStoreD02P2080(string sComment,string sDataType = "", string sBatchID = "", string sPLanCode = "", string sAStatusID = "",string dPeriodForm = null, string dPeriodTo = null, string sMethodID = "", string sStatusID = "", string sDescription = "")
       {
           string sSQL = "";
           sSQL += ("-- " + sComment + Environment.NewLine) ;
           sSQL += "Exec D02P2080 ";
           sSQL += L3SQLClient.SQLString(L3.DivisionID) + L3.COMMA; // DivisionID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(L3.UserID) + L3.COMMA; // UserID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(Environment.MachineName) + L3.COMMA; // HostID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(L3.STRLanguage) + L3.COMMA; // Language, varchar[5], NOT NULL
           sSQL += L3SQLClient.SQLNumber(L3.TranMonth) + L3.COMMA; // TranMonth, datetime, NOT NULL
           sSQL += L3SQLClient.SQLNumber(L3.TranYear) + L3.COMMA; // TranYear, datetime, NOT NULL
           sSQL += L3SQLClient.SQLString(sDataType) + L3.COMMA; // DataType, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(sBatchID) + L3.COMMA; // BatchID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(sPLanCode) + L3.COMMA; // PlanCode, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(sAStatusID) + L3.COMMA; // AStatusID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLDateSave(dPeriodForm) + L3.COMMA; // DateFrom, datetime, NOT NULL
           sSQL += L3SQLClient.SQLDateSave(dPeriodTo) + L3.COMMA; // DateTo, datetime, NOT NULL
           sSQL += L3SQLClient.SQLString(sMethodID) + L3.COMMA;
           sSQL += L3SQLClient.SQLString(sStatusID) + L3.COMMA;
           sSQL +=  "N" + L3SQLClient.SQLString(sDescription);
           return sSQL;
       }

       //#---------------------------------------------------------------------------------------------------
       //# Title: SQLStoreD91P1002
       //# Created User: 
       //# Created Date: 10/02/2023 03:06:44
       //#---------------------------------------------------------------------------------------------------
       private String SQLStoreD91P1002(string sModuleID, string sFormID, DataRow drD91P1000)
       {
           string sSQL = "";
           sSQL += ("-- Cập nhật lastkey" + Environment.NewLine);
           sSQL += "Exec D91P1002 ";
           sSQL += L3SQLClient.SQLString(L3.UserID) + L3.COMMA; // UserID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(sModuleID) + L3.COMMA; // ModuleID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(sFormID) + L3.COMMA; // FormID, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLString(drD91P1000["KeyString"]) + L3.COMMA; // KeySting, varchar[50], NOT NULL
           sSQL += L3SQLClient.SQLNumber(drD91P1000["LastKey"]); // LastKey, int, NOT NULL
           return sSQL;
       }
       #endregion

     
    }
}
