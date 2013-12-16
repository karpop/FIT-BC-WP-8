using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CleverActivityTracker.Models;
using CleverActivityTracker.ViewModels;
using System.Collections.ObjectModel;

namespace CleverActivityTracker.Views
{
    public partial class ListOfActivities : PhoneApplicationPage
    {
        pageModel model = new pageModel();
        ViewModelDbSingleton db = ViewModelDbSingleton.Instance;
        public ListOfActivities()
        {

            InitializeComponent();

            model.IsEnable = false;
            model.AllActivityItems = db.AllActivityItems;

            foreach (ApplicationBarIconButton button in ApplicationBar.Buttons)
                if (button.Text == "add")
                    button.IsEnabled = model.IsEnable;

            DataContext = model;
        }

        private class pageModel : INotifyPropertyBase
        {
            

            private bool _isEnable;
            public bool IsEnable
            {
                get { return _isEnable; }
                set { this.SetProperty(ref this._isEnable, value); }
            }

            private ObservableCollection<Activity> _allActivityItems;
            public ObservableCollection<Activity> AllActivityItems
            {
                get { return _allActivityItems; }
                set { SetProperty(ref _allActivityItems, value); }
            }
        }


        private void Img_Add_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.CreateSchedule(((sender as Image).Parent as Grid).DataContext as Activity);
            NavigationService.GoBack();
        }

        private void Img_Edit_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/EditOrNewActivity.xaml?activityId=" +
                (((sender as Image).Parent as Grid).DataContext as Activity).Id.ToString() + "&par", UriKind.Relative));
        }

        private void Img_Delete_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            db.deleteActivity((((sender as Image).Parent as Grid).DataContext as Activity));
        }

        private void ApplicationBarIconButton_Add(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/EditOrNewActivity.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Edit(object sender, EventArgs e)
        {
            model.IsEnable = model.IsEnable == true ? false : true;

            foreach (ApplicationBarIconButton button in ApplicationBar.Buttons)
                if (button.Text == "add")
                    button.IsEnabled = model.IsEnable;
        }
    }
}