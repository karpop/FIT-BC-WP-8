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

namespace CleverActivityTracker.Views
{
    public partial class MainPage : PhoneApplicationPage
    {

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);

        public event ValueChangedEventHandler ValueChanged;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        private void UserControl1_ValueChanged(object sender, System.EventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
        //private ViewModelDb db = new ViewModelDb();

        public MainPage()
        {
            InitializeComponent();
            //db.deleteDb();
            //ahoj.Text = db.ShowActivity();

            newTimer.Interval = TimeSpan.FromSeconds(1);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += OnTimerTick;

            clock.Text = string.Format("{0:00}:{1:00}:{2:00}", 0 / 3600, (0 / 60) % 60, 0 % 60);

            button21.Visibility = System.Windows.Visibility.Collapsed;
            button22.Visibility = System.Windows.Visibility.Collapsed;

            reorderListBox.DataContext = ViewModelDbSingleton.Instance.AllScheduleItems;
            MyCheckboxIndicate.DataContext = pok;
            reorderListBox2.DataContext = ViewModelDbSingleton.Instance.AllScheduleItems;
        }

        //private void Button_Click3(object sender, RoutedEventArgs e)
        //{
         //   CleverActivityTracker.Models.Activity act = db.findActivity(1);

           // act.Name = "Ahoj3";



//            db.SubmitChanges();

            //db.updateActivity(act);
            // NavigationService.Navigate(new Uri("/CleverActivityTracker;component/Views/EditOrNewActivity.xaml", UriKind.Relative));
           
        //    NavigationService.Navigate(new Uri("/Views/EditOrNewActivity.xaml", UriKind.Relative));
        //    //NavigationService.Navigate(new Uri("/Views/ListOfActivities.xaml", UriKind.Relative));
        //   //NavigationService.Navigate(new Uri("/Views/EditOrNewActivity.xaml?id=1", UriKind.Relative));
        //}

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ListOfActivities.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click_add(object sender, EventArgs e)
        { // add activity to schedlure
            //NavigationService.Navigate(new Uri("/Views/ListOfActivities.xaml?addPlan=true", UriKind.Relative));
            NavigationService.Navigate(new Uri("/Views/PivotPage1.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click_edit(object sender, EventArgs e)
        {
            if (pok.IsEnable)
                pok.IsEnable = false;
            else
                pok.IsEnable = true; 
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ListOfActivities.xaml?addPlan=true", UriKind.Relative));
        }

        private void PivotItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((Pivot)sender).SelectedIndex)
            {
                case 0:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar1"]);
                    break;

                case 1:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar2"]);
                    foreach (ApplicationBarIconButton Button in ApplicationBar.Buttons)
                    {
                        switch (Button.Text)
                        {
                            case "edit":
                                Button.Click -= ApplicationBarMenuItem_Click_edit;
                                Button.Click += ApplicationBarMenuItem_Click_edit;
                                break;
                            case "add":
                                Button.Click -= ApplicationBarMenuItem_Click_add;
                                Button.Click += ApplicationBarMenuItem_Click_add;
                                break;
                        }

                    }
                    break;
                case 2:
                    ApplicationBar = ((ApplicationBar)Application.Current.Resources["AppBar1"]);
                    break;
            }

            foreach (ApplicationBarMenuItem item in ApplicationBar.MenuItems)
            { 
                switch (item.Text)
                {
                    case "Manage activities":
                        item.Click -= ApplicationBarMenuItem_Click;
                        item.Click += ApplicationBarMenuItem_Click;
                        break;
                }
            }
        }

        NotifyEnabled pok = new NotifyEnabled { IsEnable = false };

        private void Img_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image send = (Image)sender;
            int id = 0;
            foreach (object item in ((Grid)send.Parent).Children)
            {
                switch (item.ToString())
                {
                    case "System.Windows.Controls.TextBlock":
                        switch (((TextBlock)item).Name)
                        {
                            case "Name":
                                break;
                            case "Id":
                                id = Convert.ToInt32(((TextBlock)item).Text);
                                //ViewModelDbSingleton.Instance.CreateSchedule(ViewModelDbSingleton.Instance.findActivity(Convert.ToInt32(((TextBlock)item).Text)));
                                //NavigationService.GoBack();
                                break;
                        }
                        break;
                }
            }
            switch (send.Name)
            {
                case "Img_close" :
                    ViewModelDbSingleton.Instance.deleteSchedule(id);
                    break;
                case "Img_play":
                    NavigationService.Navigate(new Uri("/Views/PivotPage1.xaml", UriKind.Relative));
                    break;
            }

        }

















        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            return;
            runAct.Visibility = System.Windows.Visibility.Visible;
            notRun.Visibility = System.Windows.Visibility.Collapsed;

            timerActivate = true;
            newTimer.Start();
            second = 0;
            BitmapImage tn = new BitmapImage();
            tn.SetSource(Application.GetResourceStream(new Uri(@"IMg/stop.png", UriKind.Relative)).Stream);
            button.Source = tn;

            button21.Visibility = System.Windows.Visibility.Visible;
            button22.Visibility = System.Windows.Visibility.Visible;
        }

        void OnTimerTick(Object sender, EventArgs args)
        {
            second++;
            clock.Text = string.Format("{0:00}:{1:00}:{2:00}", second / 3600, (second / 60) % 60, second % 60);//DateTime.Now.ToString();
        }

        public bool timerActivate = false;
        public int second = 0;
        private DispatcherTimer newTimer = new DispatcherTimer();

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox.IsChecked.Equals(true))
                MessageBox.Show("Změna času");
            if (timerActivate)
            {
                timerActivate = false;
                newTimer.Stop();
                second = 0;
                clock.Text = string.Format("{0:00}:{1:00}:{2:00}", 0 / 3600, (0 / 60) % 60, 0 % 60);
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"IMg/transport.play.png", UriKind.Relative)).Stream);
                button.Source = tn;

                button21.Visibility = System.Windows.Visibility.Collapsed;
                button22.Visibility = System.Windows.Visibility.Collapsed;

                runAct.Visibility = System.Windows.Visibility.Collapsed;
                notRun.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                timerActivate = true;
                newTimer.Start();
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"IMg/stop.png", UriKind.Relative)).Stream);
                button.Source = tn;

                button21.Visibility = System.Windows.Visibility.Visible;
                button22.Visibility = System.Windows.Visibility.Visible;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timerActivate = false;
            newTimer.Stop();
            BitmapImage tn = new BitmapImage();
            tn.SetSource(Application.GetResourceStream(new Uri(@"IMg/transport.play.png", UriKind.Relative)).Stream);
            button.Source = tn;

            button21.Visibility = System.Windows.Visibility.Collapsed;
            button22.Visibility = System.Windows.Visibility.Collapsed;
        }



    }
}