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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace CleverActivityTracker.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        ViewModelDbSingleton db = ViewModelDbSingleton.Instance;
        public MainPage()
        {
            InitializeComponent();
            //ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar1"]);
            ApplicationBar = ((ApplicationBar)this.Resources["AppBar"]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //singl.AllFolderItems[1].ActivitiesRef.Add(singl.AllActivityItems[0]);
            //ahoj.Text = singl.AllActivityGroupItems[0].ActivityRef.Name;
        }
    }
}