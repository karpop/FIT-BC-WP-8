﻿#pragma checksum "C:\Users\Karel\Documents\Visual Studio 2012\Projects\CleverActivityTracker\CleverActivityTracker\Views\PivotPage1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "55C46F8C96375878FC20F08EACF930D5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using ReorderListBox;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CleverActivityTracker.Views {
    
    
    public partial class PivotPage1 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.CheckBox MyCheckboxIndicate;
        
        internal ReorderListBox.ReorderListBox reorderListBox;
        
        internal System.Windows.DataTemplate reorderListBoxTemplate;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CleverActivityTracker;component/Views/PivotPage1.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MyCheckboxIndicate = ((System.Windows.Controls.CheckBox)(this.FindName("MyCheckboxIndicate")));
            this.reorderListBox = ((ReorderListBox.ReorderListBox)(this.FindName("reorderListBox")));
            this.reorderListBoxTemplate = ((System.Windows.DataTemplate)(this.FindName("reorderListBoxTemplate")));
        }
    }
}

