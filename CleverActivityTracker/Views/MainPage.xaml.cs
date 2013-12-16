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
        PageModel model = new PageModel();
        public MainPage()
        {
            InitializeComponent();

            model.ScheduleEditEnabled = false;

            model.AllScheduleItems = db.AllScheduleItems;
            model.AllHistoryItems = db.AllHistoryItems;

            DataContext = model;

            //ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar1"]);
            //ApplicationBar = ((ApplicationBar)this.Resources["AppBar"]);
        }

        private void PivotItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = ((ApplicationBar)this.Resources["AppBarTracking"]);
                    break;
                case 1:
                    ApplicationBar = ((ApplicationBar)this.Resources["AppBarSchedule"]);
                    foreach (ApplicationBarIconButton button in ApplicationBar.Buttons)
                        if (button.Text == "add")
                            button.IsEnabled = false;
                    break;
                case 2:
                    ApplicationBar = ((ApplicationBar)this.Resources["AppBarHistory"]);
                    break;
            }
        }


        private class PageModel : INotifyPropertyBase
        {
            private bool _scheduleEditEnabled;
            public bool ScheduleEditEnabled
            {
                get { return _scheduleEditEnabled; }
                set { SetPropertyChanged(ref _scheduleEditEnabled, value); }
            }

            private bool _historyEditEnabled;
            public bool HistoryEditEnabled
            {
                get { return _historyEditEnabled; }
                set { SetPropertyChanged(ref _historyEditEnabled, value); }
            }

            private ObservableCollection<Schedule> _allScheduleItems;
            public ObservableCollection<Schedule> AllScheduleItems
            {
                get { return _allScheduleItems; }
                set { this.SetPropertyChanged(ref _allScheduleItems, value); }
            }

            private ObservableCollection<History> _allHistoryItems;
            public ObservableCollection<History> AllHistoryItems
            {
                get { return _allHistoryItems; }
                set { this.SetPropertyChanged(ref _allHistoryItems, value); }
            }
        }

        private void ApplicationBarIconButton_Add(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ListOfActivities.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Edit(object sender, EventArgs e)
        {
            model.ScheduleEditEnabled = model.ScheduleEditEnabled == true ? false : true;
            foreach (ApplicationBarIconButton button in ApplicationBar.Buttons)
                if (button.Text == "add")
                    button.IsEnabled = model.ScheduleEditEnabled;
        }

        private void Schedule_Img_Play_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (db.IsRunningActivity())
                MessageBox.Show("It is not possible to run two activities at the same time.");
            else
                db.RunActivity((((sender as Image).Parent as Grid).DataContext as Schedule).ActivityRef);
        }

        private void Schedule_Img_Delete_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.deleteSchedule((((sender as Image).Parent as Grid).DataContext as Schedule));
        }

        private void listBox_AllScheduleItems_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            db.SaveOrderSchedule();
        }
    }
}