   M S B A M L   `   `   `  ���� D  @D99D0451, Version=4.10.0.0, Culture=neutral, PublicKeyToken=nullQ6clr-namespace:Lemon3.Controls.DevExp;assembly=D99D0451Lemon3.Controls.DevExp  D @DA2D1050, Version=4.10.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DA2D1050DA2D1050    DA2D1050.D26F1051   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     EL36clr-namespace:Lemon3.Controls.DevExp;assembly=D99D0451     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     f bDevExpress.Xpf.Grid.v17.1.Core, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  "localclr-namespace:DA2D1050  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     N� Title$2  ,Chính sách lương hiệu quả - D26F1051��$	��680��$
��1024�� � 	Resources  �� MergedDictionaries �� Source��  �� $9 3/DA2D1050;component/Templates/ControlTemplates.xaml?��� $8 2/D99D0451;component/Templates/Lemon3Templates.xaml?�%    .���  � RowDefinitions �� $��30 ��� �� $��40 �.+�)  #DevExpress.Xpf.Bars.MainMenuControl  -     Name$ MainMenuControl1��$��Stretch=�$��Center=�$��0��$��0��$��2��,  &DevExpress.Xpf.Bars.ToolBarControlBase   Items. '  !DevExpress.Xpf.Bars.BarButtonItem  -     Name$ tsbAdd��!  DevExpress.Xpf.Bars.BarItem   Content)  #DevExpress.Xpf.Core.ObjectConverter$ Thêm   -   $ tsbEdit��$ Sửa   -   $ 	tsbDelete��$
 Xóa *  $DevExpress.Xpf.Bars.BarItemSeparator    -   $ 
tsbListAll��$ Liệt kê tất cả     -   $ 
tsbSysInfo��$ Thông tin hệ thống � $��1��	 � ColumnDefinitions	 �� $��1.22* ��� $��2* �.+�*   $Lemon3.Controls.DevExp.L3GridControl  -   
  Name$

 tdbg��$��0��$��1��)  #DevExpress.Xpf.Grid.DataControlBase   SelectionMode$	 Row=�   AutoGenerateColumns$
 None=�#   EnableSmartColumnsGeneration .  � ContextMenu �� -	   $��ContextMenu1��.@�m� -
   $��mnsAdd��$��Thêm��m� -   $��mnsEdit��$��Sửa��m� -   $��	mnsDelete��$
��Xóa���� m� -   $��
mnsApprove��$��Duyệt���� m� -   $��
mnsListAll��$��Liệt kê tất cả���� m� -   $��
mnsSysInfo��$��Thông tin hệ thống��%	  DevExpress.Xpf.Grid.GridControl 	  View #
  DevExpress.Xpf.Grid.TableView
  -    
 Name$ tdbgTableView��&   DevExpress.Xpf.Grid.DataViewBase   ShowFilterPanelMode$ Never=� 
  AllowPerPixelScrolling .    ShowTotalSummary .     AllowEditing .   
  ShowAutoFilterRow .  	  Columns. $  DevExpress.Xpf.Grid.GridColumn  -     Name$ COL_IsApprove��$  DevExpress.Xpf.Grid.ColumnBase   	FieldName$ 	IsApprove��$  DevExpress.Xpf.Grid.BaseColumn   Header$ Duyệt    UnboundType]	 YDevExpress.Data.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a8 	 2DevExpress.Utils.Design.UnboundColumnTypeConverter$ Boolean    Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$	 50*    MinWidth$ 50��'    HorizontalHeaderContentAlignment$ Center=�   EditSettings 7  1DevExpress.Xpf.Editors.Settings.CheckEditSettings    -   $ COL_CommBenefitID��$ Mã chính sách $ CommBenefitID��$ Center=�$	 110   -   $ COL_Description��$ Diễn giải $ Description��$ Center=�$	 170   -   $ COL_ProjectID��$ Dự án $ 	ProjectID��$ Center=�$	 110   -   $ COL_ProjectName��$ Tên dự án $ ProjectName��$ Center=�$	 170   -   $ COL_ValidDateFrom��$ Hiệu lực từ $ ValidDateFrom��$ Center=�$	 110   -   $ COL_ValidDateTo��$ Hiệu lực đến $ ValidDateTo��$ Center=�$	 110   -   $ COL_PropertyGroupID��$ PropertyGroupID��$ Nhóm sản phẩm $ Center=�$	 110   -   $ COL_PropertyGroupName��$ PropertyGroupName��$ Tên nhóm sản phẩm $ Center=�$	 110 � $��1��$��1��$��5,0,5,0q�.+�'   !Lemon3.Controls.DevExp.L3GroupBox  -     Name$
 grpD��$��10,8,8,5q����� -     ��Name$  
TextBlock2��$
��Bold"�
Chi tiết.��� -   $��gridD�� �� $	��150 ��� .+�� .+�$   Lemon3.Controls.DevExp.L3Label  -   !  Name$! lblCommBenefitID��$��Mã chính sách��$
��Left=�$	��Top=�'   !Lemon3.Controls.DevExp.L3TextEdit  -   "  Name$" txtCommBenefitID��$
��Left=�$��95,0,0,0q�$	��Top=�$	��150��(   "Lemon3.Controls.DevExp.L3CheckEdit  -   #  Name$# chkIsApprove��&   DevExpress.Xpf.Editors.CheckEdit$   Content$$ Duyệt $
��Left=�$	��Top=�$��	255,0,0,0q�  -    $! lblDescription��$��Diễn giải��$
��Left=�$��0,30,0,0q�$	��Top=�$	��150��  -!   $" txtDescription��$��	95,30,0,0q�$	��Top=�  -"   $! lblProjectID��$��Dự án��$
��Left=�$��0,60,0,0q�$	��Top=�)   #Lemon3.Controls.DevExp.L3LookUpEdit  -#   %  Name$% tdbcProjectID��*  $DevExpress.Xpf.Editors.PopupBaseEdit&   
PopupWidth$	& 350Q�+  %DevExpress.Xpf.Grid.LookUp.LookUpEdit'   AutoPopulateColumns' .  $
��Left=�$��	95,60,0,0q�$	��Top=�+  %DevExpress.Xpf.Editors.LookUpEditBase(   ValueMember$( 	ProjectID��)   DisplayMember$) 	ProjectID��$	��150��   ProjectIDResource*   PopupContentTemplate#* �     -$   $" txtProjectName��$	��Top=�$��
255,60,0,0q�  -%   $! lblValidDate��$��Hiệu lực��$
��Left=�$	��Top=�$��0,90,0,0q�'   !Lemon3.Controls.DevExp.L3DateEdit  -&   +  Name$+ c1ValidDateFrom��$	& 350Q�$
��Left=�$��	95,90,0,0q�$	��Top=�$	��150��  -'   $+ c1ValidDateTo��$	& 350Q�$
��Left=�$��
255,90,0,0q�$	��Top=�$	��150��  -(   $! lblPropertyGroupID��$��Nhóm sản phẩm��$
��Left=�$	��Top=�$��	0,120,0,0q�  -)   $% tdbcPropertyGroupID��$	& 350Q�' .  $
��Left=�$��
95,120,0,0q�$	��Top=�$( PropertyGroupID��$) PropertyGroupID��$	��150��  PropertyGroupIDResource#* �    -*   $" txtPropertyGroupName��$	��Top=�$��255,120,0,0q�&   DevExpress.Xpf.Core.DXTabControl  $��0��$��2��$��0,2,0,0q�$��5q�.@�#  DevExpress.Xpf.Core.DXTabItem  -+   ,  Name$, tabItem1���  �DevExpress.Xpf.Core.Native.HeaderedSelectorItemBase`2[[DevExpress.Xpf.Core.DXTabControl, DevExpress.Xpf.Core.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a],[DevExpress.Xpf.Core.DXTabItem, DevExpress.Xpf.Core.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a]]-   Header$- 1. Trực tiếp��.��� 
������  �� $��3* ��� $��2* ��� $��0.65* �.+�  -,   $
 tdbgD1��$	 Row=�$
 None=� .  
  --   $ tdbgTableView1��$ Never=� .  .   .  &   DevExpress.Xpf.Grid.GridViewBase.   ShowGroupPanel. .  .   -.   $ COL1_OrderNum��$	 STT $ OrderNo��$ Center=�$ 50��$	 50*   -/   $ COL1_CommObjectName��$ Đối tượng $ CommObjectName��$ Center=�$	 170��$
 170*  1    +Lemon3.Controls.DevExp.L3LookUpEditSettings   -0   / �Name$/ tdbdCommObjectID1��<!  6DevExpress.Xpf.Editors.Settings.LookUpEditSettingsBase0 !  AutoComplete0 . 1 !  ImmediatePopup1 . 3"  -DevExpress.Xpf.Grid.LookUp.LookUpEditSettings2 "  AutoPopulateColumns2 .  3 !  DisplayMember$3 CommObjectName��4 !  ValueMember$4 CommObjectName��  CommObjectIDResource;#  5DevExpress.Xpf.Editors.Settings.PopupBaseEditSettings5 #  PopupContentTemplate#5 �    -1   $ COL1_CommRate��$ Bán hàng trực tiếp $ CommRate��$ Center=�$	 110��$
 110*   -2   $ COL1_CommAmount��$# Trường hợp đặc biệt $ 
CommAmount��$ Center=�$	 170��$
 170*   -3   $ COL1_OrderNo��$ OrderNo��6   Visible6 .    -4   $ COL1_CommObjectID��$ CommObjectID��6 .    -5   $ COL1_TransID��$ TransID��6 .    -6   $
 tdbgD2��$��1��$��0,8,0,0q�$	 Row=�$
 None=� .  
  -7   $ tdbgTableView2��$ Never=� .  .   .  . .  .   -8   $ COL2_And_OrName��$ And/Or $ 
And_OrName��$ Center=�$ 50��$	 50*     -9   $/ tdbdAnd_OrID1��0 . 1 . 2 .  $3 
And_OrName��$4 
And_OrName��  And_OrIDResource#5 �    -:   $ COL2_ConditionName��$ Điều kiện $ ConditionName��$ Center=�$	 170��$
 170*     -;   $/ tdbdConditionID1��0 . 1 . 2 .  $3 ConditionName��$4 ConditionName��  ConditionIDResource#5 �    -<   $ COL2_OperatorName��$ 
Toán tử $ OperatorName��$ Center=�$	 110��$
 110*     -=   $/ tdbdOperatorID1��0 . 1 . 2 .  $3 OperatorName��$4 OperatorName��  OperatorIDResource#5 �    ->   $ COL2_ValueFrom��$ Giá trị từ $ 	ValueFrom��$ Center=�$	 110��$
 110*     -?   $/ tdbdValueFrom��0 . 1 . 2 .  $3 ID��$4 
Descripton��  ValueFromIDResource#5 �    -@   $ COL2_ValueTo��$ Giá trị đến $ ValueTo��$ Center=�$	 110��$
 110*   -A   $ COL2_OrderNo��$ OrderNo��6 .    -B   $ COL2_And_Or��$ And_Or��6 .    -C   $ COL2_ConditionID��$ ConditionID��6 .    -D   $ COL2_DataType��$ DataType��6 .    -E   $ COL2_FormatNum��$ 	FormatNum��6 .    -F   $ COL2_EnableOperator��$ EnableOperator��6 .    -G   $ COL2_OperatorID��$ 
OperatorID��6 .    -H   $ COL2_EnableValueFrom��$ EnableValueFrom��6 .    -I   $ COL2_EnableValueTo��$ EnableValueTo��6 .    -J   $ COL2_TransID��$ TransID��6 .    $��2��$��Điều kiện��$��5,8,5,5q�  -K   $" txtFormulaDesc��$��2��$��65,8,0,0q�)$  #DevExpress.Xpf.Editors.TextEditBase7 $  TextWrapping$
7 Wrap=�%%  DevExpress.Xpf.Editors.BaseEdit8 %  
IsReadOnly8 .   -L   $, tabItem2��$+- %2. Đợt nhận lương hiệu quả��.���  �� $��3* ��� $��2* ��� $��0.65* �.+�� $��0��	 �� $��1.25* ��� $��2* �.+�  -M   $
 tdbgD3��$	 Row=�$
 None=� .  
  -N   $ tdbgTableView3�� .  .   .  . .  .   -O   $ COL3_CommObjectName��$ Đối tượng $ CommObjectName��$ Center=�$
 110*     -P   $/ tdbdCommObjectID2��0 . 1 . 2 .  $3 CommObjectName��$4 CommObjectName��#5 �    -Q   $ COL3_CommObjectID��$ CommObjectID��6 .    -R   $ COL3_OrderNo��$ OrderNo��6 .    -S   $
 tdbgD4��$	 Row=�$
 None=� . $��1��$��5,0,0,0q� 
  -T   $ tdbgTableView4��$ Never=� .  .   .  . .  .   -U   $ 
COL4_Stage��$ Đợt nhận $ Stage��$ Center=�$	 80*   -V   $ COL4_StageReceive��$ Mức nhận $ StageReceive��$ Center=�$	 80*   -W   $ COL4_Margin��$ Biên độ $ Margin��$ Center=�$	 80*   -X   $ COL4_Description��$ Diễn giải $ Description��$ Center=�$
 170*   -Y   $ COL4_CommObjectID��$ CommObjectID��6 .    -Z   $ COL4_OrderNo��$ OrderNo��6 .    -[   $
 tdbgD5��$	 Row=�$
 None=� . $��1��$��0,8,0,0q� 
  -\   $ tdbgTableView5�� .  .   .  . .  .   -]   $ COL5_And_OrName��$ And/Or $ 
And_OrName��$ Center=�$	 50*     -^   $/ tdbdAnd_OrID2��0 . 1 . 2 .  $3 
And_OrName��$4 
And_OrName��#5 �    -_   $ COL5_ConditionName��$ Điều kiện $ ConditionName��$ Center=�$
 170*     -`   $/ tdbdConditionID2��0 . 1 . 2 .  $3 ConditionName��$4 ConditionName��#5 �    -a   $ COL5_OperatorName��$ 
Toán tử $ OperatorName��$ Center=�$
 110*     -b   $/ tdbdOperatorID2��0 . 1 . 2 .  $3 OperatorName��$4 OperatorName��#5 �    -c   $ COL5_ValueFrom��$ Giá trị từ $ 	ValueFrom��$ Center=�$
 110*   -d   $ COL5_ValueTo��$ Giá trị đến $ ValueTo��$ Center=�$
 110*   -e   $ COL5_OrderNo��$ OrderNo��6 .    -f   $ COL5_And_Or��$ And_Or��6 .    -g   $ COL5_ConditionID��$ ConditionID��6 .    -h   $ COL5_DataType��$ DataType��6 .    -i   $ COL5_FormatNum��$ 	FormatNum��6 .    -j   $ COL5_EnableOperator��$ EnableOperator��6 .    -k   $ COL5_OperatorID��$ 
OperatorID��6 .    -l   $ COL5_EnableValueFrom��$ EnableValueFrom��6 .    -m   $ COL5_EnableValueTo��$ EnableValueTo��6 .    $��2��$��Điều kiện��$��5,8,5,5q�  -n   $" txtFormulaDesc1��$��2��$��65,8,0,0q�$
7 Wrap=�8 . � $��2��.+�  -o   $# chkShowIsApprove��$2$ ,Chỉ hiển thị dữ liệu chưa duyệt $��10,0,0,0q�$
��Left=�$��Center=�+&   %Lemon3.Controls.DevExp.L3SimpleButton&  -p   9 & Name$9 btnSave��$
��Lưu��$��Right=�$��	0,6,260,6q�$��28��$��Center=�$��76��&  -q   $9 btnNext��$��Lưu và nhập tiếp��$��Right=�$��	0,6,121,6q�$��28��$��Center=�$	��135��&  -r   $9 
btnNotSave��$��Không lưu��$��Right=�$��0,0,10,0q�$��27��$��Center=�$	��107��