   M S B A M L   `   `   `  ���� D  @D99D0451, Version=4.10.0.0, Culture=neutral, PublicKeyToken=nullQ6clr-namespace:Lemon3.Controls.DevExp;assembly=D99D0451Lemon3.Controls.DevExp  D @D31D4050, Version=4.10.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:D31D4050D31D4050    D31D4050.D31F3420   a ]DevExpress.Xpf.Core.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     EL36clr-namespace:Lemon3.Controls.DevExp;assembly=D99D0451     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     f bDevExpress.Xpf.Grid.v17.1.Core, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   <dxc2http://schemas.devexpress.com/winfx/2008/xaml/core  ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  "localclr-namespace:D31D4050  -     N� Title$5  /Tình hình sử dụng NVL theo Lô - D31F3420��$	��680��$
��1024�� � 	Resources  �� MergedDictionaries �� Source��  �� $9 3/D31D4050;component/Templates/ControlTemplates.xaml?��� $8 2/D99D0451;component/Templates/Lemon3Templates.xaml?�%    .���  � RowDefinitions �� $��40 ��� $��94 ��� �� $��35 �.+�� $��5,10,0,0q� � ColumnDefinitions �� �� �� �� $	��100 �.+�$   Lemon3.Controls.DevExp.L3Label  -     Name$ lblInventoryTypeID��$��Loại hàng��$
��Left=�$��5,5,0,0q�$	��Top=�)   #Lemon3.Controls.DevExp.L3LookUpEdit  -     Name$ tdbcInventoryTypeID��*  $DevExpress.Xpf.Editors.PopupBaseEdit   
PopupWidth$	 350Q�+  %DevExpress.Xpf.Grid.LookUp.LookUpEdit	   AutoPopulateColumns	 .  $��	95,5,10,0q�$	��Top=�+  %DevExpress.Xpf.Editors.LookUpEditBase
   ValueMember$
 InventoryTypeID��   DisplayMember$ InventoryTypeName��   InventoryTypeIDResource   PopupContentTemplate# �     -   $ lblLocationNo��$	��Lô��$
��Left=�$	��Top=�$��1��$��10,5,0,0q�  -   $ tdbcLocationNo��$	 350Q�	 .  $��1��$��	95,5,10,0q�$	��Top=�$
 
LocationNo��$ 
LocationNo��  LocationNoResource# �    -   $ lblInventoryID��$��Mã NVL��$
��Left=�$	��Top=�$��2��$��10,5,0,0q�'   !Lemon3.Controls.DevExp.L3TextEdit  -     Name$ txtInventoryID��$��2��$��	95,5,10,0q�$	��Top=�%  DevExpress.Xpf.Editors.BaseEdit   
IsReadOnly . +   %Lemon3.Controls.DevExp.L3SimpleButton  -     Name$ 	btnFilter��$��Lọc��$��3��$��0,0,5,0q�$��Right=�$��28��$��Center=�$��90��*	   $Lemon3.Controls.DevExp.L3GridControl	  -	    	 Name$ tdbgM��$��1��$��5,10,5,5q�)
  #DevExpress.Xpf.Grid.DataControlBase 
  SelectionMode$	 Row=� 
  AutoGenerateColumns$
 None=�# 
  EnableSmartColumnsGeneration .  $  DevExpress.Xpf.Grid.GridColumn �� 
TargetType$  DevExpress.Xpf.Grid.BaseColumn �� Property �� Value*  $DevExpress.Xpf.Grid.GridColumnHeader��%   '        '  H     �� "  .��� ( �@  HorizontalHeaderContentAlignment$ Center=��� "  .���  � ��$ 40��%  DevExpress.Xpf.Grid.GridControl   View #  DevExpress.Xpf.Grid.TableView  -
     Name$ tdbgMTableView��&   DevExpress.Xpf.Grid.DataViewBase   ShowFilterPanelMode$ Never=�   AllowPerPixelScrolling .    ShowTotalSummary .     AllowEditing .  &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     AllowColumnMoving .  $   HorizontalScrollbarVisibility$ Visible=�    Columns.    -   !  Name$! COLM_MLocationNo��"   Header)  #DevExpress.Xpf.Core.ObjectConverter$" Lô NVL $  DevExpress.Xpf.Grid.ColumnBase#   	FieldName$# MLocationNo��'$    HorizontalHeaderContentAlignment$$ Center=�%   MinWidth$	% 110��&   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$
& 110*   -   $! COLM_InventoryID��$" Mã NVL $# InventoryID��$$ Center=�$	% 110��$
& 110*   -   $! COLM_InventoryName��$" Tên NVL $# InventoryName��$$ Center=�$	% 140��$
& 140*   -   $! COLM_UnitID��$
" ĐVT $# UnitID��$$ Center=�$	% 110��$
& 110*   -   $! COLM_TotalOQTY��$" SL cả lô $# 	TotalOQTY��$$ Center=�$	% 110��$
& 110*   -   $! COLM_TotalCQTY2��$ " SL cả lô (ĐVT cấp 2) $# 
TotalCQTY2��$$ Center=�$	% 110��$
& 110* '   HeaderTemplate' �� .R��� .+��� $
��Wrap=�( �� TextAlignment$( Center=�����   -   $! COLM_UsedOQTY��$" SL đã tiêu hao $# UsedOQTY��$$ Center=�$	% 110��$
& 110* ' �� .R��� .+��� $( Center=�����   -   $! COLM_UsedCQTY2��$&"  SL đã tiêu hao (ĐVT cấp 2) $# 	UsedCQTY2��$$ Center=�$	% 110��$
& 110* ' �� .R��� .+��� $
��Wrap=�$( Center=�����   -   $! COLM_RemainOQTY��$" SL còn lại $# 
RemainOQTY��$$ Center=�$	% 110��$
& 110* ' �� .R��� .+��� $( Center=�����   -   $! COLM_RemainCQTY2��$"" SL còn lại (ĐVT cấp 2) $# RemainCQTY2��$$ Center=�$	% 110��$
& 110* ' �� .R��� .+��� $
��Wrap=�$( Center=����� � $��2��.+�  -   $ 	lblDetail��$��
Chi tiết��$��10,5,5,5q�$
��Left=�$	��Top=�$��Italic$�	  -   $ tdbgD��$��	5, 27,5,5q�$	 Row=�$
 None=� .    -   $ tdbgDTableView��$ Never=� .  .   .  )   ShowAutoFilterRow) . *   FormatConditions* )  #DevExpress.Xpf.Grid.FormatCondition  -  'DevExpress.Xpf.Grid.FormatConditionBase+   
Expression$+ [Style] = '(B, Red)'��1  +DevExpress.Xpf.Grid.ExpressionConditionBase,   Format., 6  0DevExpress.Xpf.Core.ConditionalFormatting.Format  -   
FontWeight$
- Bold"�.   
Foreground
. �  ��  $+ [Style] = '(B, Black)'��.,   $
- Bold"�
. �   �  $+ [Style] = '(I, Blue)'��.,   /   	FontStyle$/ Italic$�
. ��  �  $+ [Style] = '(I, Black)'��.,   $/ Italic$�
. �   �.    -   $! 
COLD_Style��$" Style $# Style��$$ Center=�$	& 110 0   Visible0 .    -   $! COLD_OrderNo��$# OrderNo��$	" STT $	& 50* $% 50��$$ Center=�1   EditSettings1 6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!2   HorizontalContentAlignment$2 Center=�  -   $! COLD_StatisticVoucherNo��$" Số phiếu thống kê $# StatisticVoucherNo��$$ Center=�$	& 110   -   $! COLD_MachineLeaderName��$" Trưởng máy $# MachineLeaderName��$$ Center=�$	& 150   -   $! COLD_StatisticDate��$" Ngày thống kê $# StatisticDate��$$ Center=�$	& 110   -   $! COLD_ShiftID��$" Ca $# ShiftID��$$ Center=�$	& 140   -   $! COLD_ProductID��$" Mã Thành phẩm $# 	ProductID��$$ Center=�$	& 110   -   $! COLD_ProductName��$" Tên thành phẩm $# ProductName��$$ Center=�$	& 140   -    $! COLD_ProductOQTY��$# ProductOQTY��$" SL thành phẩm $$ Center=�$	& 110   -!   $! COLD_ProductCQTY2��$# ProductCQTY2��$%" SL thành phẩm (ĐVT cấp 2) $$ Center=�$	& 110 ' �� .R��� .+��� $
��Wrap=�$( Center=�����   -"   $! COLD_WP_OQTY��$# WP_OQTY��$" SL phế phẩm $$ Center=�$	& 110   -#   $! COLD_WP_CQTY2��$# WP_CQTY2��$$" SL phế phẩm (ĐVT cấp 2) $$ Center=�$	& 110 ' �� .R��� .+��� $
��Wrap=�$( Center=�����   -$   $! COLD_WM_OQTY��$# WM_OQTY��$" SL phế liệu $$ Center=�$	& 110   -%   $! COLD_WM_CQTY2��$# WM_CQTY2��$$" SL phế liệu (ĐVT cấp 2) $$ Center=�$	& 110 ' �� .R��� .+��� $
��Wrap=�$( Center=�����   -&   $! COLD_TotalQuantity��$# TotalQuantity��$" SL đã tiêu hao $$ Center=�$	& 110   -'   $! COLD_TotalQuantity2��$# TotalCQuantity2��$&"  SL đã tiêu hao (ĐVT cấp 2) $$ Center=�$	& 110 ' �� .R��� .+��� $
��Wrap=�$( Center=�����   -(   $! COLD_Description��$# Description��$" Diễn giải $$ Center=�$	& 170   -)   $ btnPrint��$��In��$��3��$��Right=�$��0,0,5,0q�$��28��$��Center=�$��90��