﻿#pragma checksum "C:\Users\Karel\Documents\Visual Studio 2012\Projects\CleverActivityTracker\CleverActivityTracker\Views\EditOrNewActivity.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3D3EBF9934B7B86B6D87BED013A1D5B1"
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
using Microsoft.Phone.Shell;
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
    
    
    public partial class EditOrNewActivity : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Grid GridEdit;
        
        internal System.Windows.Controls.TextBox Activity_Name;
        
        internal System.Windows.Controls.Grid GridAddNew;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton delete;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/CleverActivityTracker;component/Views/EditOrNewActivity.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.GridEdit = ((System.Windows.Controls.Grid)(this.FindName("GridEdit")));
            this.Activity_Name = ((System.Windows.Controls.TextBox)(this.FindName("Activity_Name")));
            this.GridAddNew = ((System.Windows.Controls.Grid)(this.FindName("GridAddNew")));
            this.delete = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("delete")));
        }
    }
}

