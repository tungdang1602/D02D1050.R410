﻿#pragma checksum "..\..\..\Forms\D26F5556.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B7E468E49CB3ABDFCAC08E1C4E1CC53412680FCC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DA2D1050;
using DevExpress.Core;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.ConditionalFormatting;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid.TreeList;
using Lemon3.Controls.DevExp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DA2D1050 {
    
    
    /// <summary>
    /// D26F5556
    /// </summary>
    public partial class D26F5556 : Lemon3.Controls.DevExp.L3Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Forms\D26F5556.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Lemon3.Controls.DevExp.L3GridControl tdbg;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Forms\D26F5556.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.TableView tdbgTableView;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Forms\D26F5556.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn COL_IsChoose;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Forms\D26F5556.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn COL_ID;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Forms\D26F5556.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn COL_Description;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DA2D1050;component/forms/d26f5556.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forms\D26F5556.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\Forms\D26F5556.xaml"
            ((DA2D1050.D26F5556)(target)).Loaded += new System.Windows.RoutedEventHandler(this.L3Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tdbg = ((Lemon3.Controls.DevExp.L3GridControl)(target));
            
            #line 16 "..\..\..\Forms\D26F5556.xaml"
            this.tdbg.FilterChanged += new System.Windows.RoutedEventHandler(this.tdbg_FilterChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tdbgTableView = ((DevExpress.Xpf.Grid.TableView)(target));
            
            #line 20 "..\..\..\Forms\D26F5556.xaml"
            this.tdbgTableView.FocusedRowChanged += new DevExpress.Xpf.Grid.FocusedRowChangedEventHandler(this.tdbgTableView_FocusedRowChanged);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\Forms\D26F5556.xaml"
            this.tdbgTableView.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.tdbgTableView_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\Forms\D26F5556.xaml"
            this.tdbgTableView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.tdbgTableView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.COL_IsChoose = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 5:
            this.COL_ID = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 6:
            this.COL_Description = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

