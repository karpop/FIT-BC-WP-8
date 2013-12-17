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
            model.IsRunningActivity = false;

            model.AllScheduleItems = db.AllScheduleItems;
            model.AllHistoryItems = db.AllHistoryItems;

            DataContext = model;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();

            //ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar1"]);
            //ApplicationBar = ((ApplicationBar)this.Resources["AppBar"]);
        }

        DispatcherTimer timer = new DispatcherTimer();
        void OnTimerTick(Object sender, EventArgs args)
        {
            if (db.runningActivity != null)
                activityTime.Text = (DateTime.Now - db.runningFrom).ToString(@"hh\:mm\:ss");
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
            private bool _isRunningActivity;
            public bool IsRunningActivity
            {
                get { return _isRunningActivity; }
                set { SetPropertyChanged(ref _isRunningActivity, value); }
            }

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
            mainPagePivot.SelectedIndex = 0;
            if (db.IsRunningActivity())
                MessageBox.Show("It is not possible to run two activities at the same time.");
            else
            {
                model.IsRunningActivity = true;
                db.RunActivity((((sender as Image).Parent as Grid).DataContext as Schedule).ActivityRef);
                db.deleteSchedule((((sender as Image).Parent as Grid).DataContext as Schedule));
                activityName.Text = db.runningActivity.Name;
            }
                

            mainPagePivot.SelectedIndex = 0;
        }

        private void Schedule_Img_Delete_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.deleteSchedule((((sender as Image).Parent as Grid).DataContext as Schedule));
        }

        private void listBox_AllScheduleItems_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            db.SaveOrderSchedule();
        }

        private void Img_Main_Stop_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.StopActivity();
            model.IsRunningActivity = false;
            activityTime.Text = "00:00:00";
        }

        private void Img_Main_Play_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.RunActivity(db.runningActivity);
            columDef.Width = new GridLength(60);
            Img_Main_Stop.Visibility = System.Windows.Visibility.Visible;
            Img_Main_Play.Visibility = System.Windows.Visibility.Collapsed;
            timer.Start();
        }

        private void Img_Main_Pause_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.PauseActivity();
            columDef.Width = new GridLength(0);
            Img_Main_Stop.Visibility = System.Windows.Visibility.Collapsed;
            Img_Main_Play.Visibility = System.Windows.Visibility.Visible;
            timer.Stop();
            activityTime.Text = "00:00:00";
        }
    }
}