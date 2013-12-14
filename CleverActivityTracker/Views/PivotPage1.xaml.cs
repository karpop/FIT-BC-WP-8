using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CleverActivityTracker.ViewModels;
using CleverActivityTracker.Models;

namespace CleverActivityTracker.Views
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        ViewModelDbSingleton dbSingleton = ViewModelDbSingleton.Instance;

        NotifyEnabled pok = new NotifyEnabled { IsEnable = false };

        public PivotPage1()
        {
            InitializeComponent();

            reorderListBox.DataContext = dbSingleton.AllActivityItems;

            MyCheckboxIndicate.DataContext = pok;
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //TextBlock send = (TextBlock)sender;

            //Grid parent = (Grid)send.Parent;

            ////MessageBox.Show(((TextBlock)parent.Children.Last()).Text);
            ////ViewModelDbSingleton.Instance.CreateSchedule(Convert.ToInt32(((TextBlock)parent.Children.Last()).Text));
            //ViewModelDbSingleton.Instance.CreateSchedule(ViewModelDbSingleton.Instance.findActivity(1));
            //ViewModelDbSingleton.Instance.CreateSchedule(ViewModelDbSingleton.Instance.findActivity(Convert.ToInt32(((TextBlock)parent.Children.Last()).Text)));
            //NavigationService.GoBack();
        }

        private void Img_add_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            dbSingleton.CreateSchedule((sender as Image).DataContext as Activity);
            NavigationService.GoBack();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            pok.IsEnable = pok.IsEnable ? false : true;
        }
    }
}