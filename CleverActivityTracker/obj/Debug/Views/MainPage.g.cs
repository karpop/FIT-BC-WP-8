﻿#pragma checksum "C:\Users\Karel\Documents\FIT-BC-WP-8\CleverActivityTracker\Views\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "72BD1676477BBD34727AD4F893665A92"
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
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot mainPagePivot;
        
        internal System.Windows.Controls.TextBlock runningActivity;
        
        internal System.Windows.Controls.TextBlock activityName;
        
        internal System.Windows.Controls.TextBlock activityTime;
        
        internal System.Windows.Controls.ColumnDefinition columDef;
        
        internal System.Windows.Controls.Image Img_Main_Stop;
        
        internal System.Windows.Controls.Image Img_Main_Play;
        
        internal System.Windows.Controls.Image Img_Main_Pause;
        
        internal System.Windows.Controls.CheckBox Checkbox;
        
        internal ReorderListBox.ReorderListBox listBox_AllScheduleItems2;
        
        internal System.Windows.Controls.CheckBox Checkbox1;
        
        internal ReorderListBox.ReorderListBox listBox_AllScheduleItems;
        
        internal ReorderListBox.ReorderListBox listBox_AllHistoryItems;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/CleverActivityTracker;component/Views/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.mainPagePivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("mainPagePivot")));
            this.runningActivity = ((System.Windows.Controls.TextBlock)(this.FindName("runningActivity")));
            this.activityName = ((System.Windows.Controls.TextBlock)(this.FindName("activityName")));
            this.activityTime = ((System.Windows.Controls.TextBlock)(this.FindName("activityTime")));
            this.columDef = ((System.Windows.Controls.ColumnDefinition)(this.FindName("columDef")));
            this.Img_Main_Stop = ((System.Windows.Controls.Image)(this.FindName("Img_Main_Stop")));
            this.Img_Main_Play = ((System.Windows.Controls.Image)(this.FindName("Img_Main_Play")));
            this.Img_Main_Pause = ((System.Windows.Controls.Image)(this.FindName("Img_Main_Pause")));
            this.Checkbox = ((System.Windows.Controls.CheckBox)(this.FindName("Checkbox")));
            this.listBox_AllScheduleItems2 = ((ReorderListBox.ReorderListBox)(this.FindName("listBox_AllScheduleItems2")));
            this.Checkbox1 = ((System.Windows.Controls.CheckBox)(this.FindName("Checkbox1")));
            this.listBox_AllScheduleItems = ((ReorderListBox.ReorderListBox)(this.FindName("listBox_AllScheduleItems")));
            this.listBox_AllHistoryItems = ((ReorderListBox.ReorderListBox)(this.FindName("listBox_AllHistoryItems")));
        }
    }
}

