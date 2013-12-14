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
using System.Collections.ObjectModel;
using CleverActivityTracker.Models;

namespace CleverActivityTracker.Views
{
    public partial class ListOfActivities : PhoneApplicationPage
    {
        ViewModelDbSingleton dbSingleton = ViewModelDbSingleton.Instance;

        NotifyEnabled pok = new NotifyEnabled { IsEnable = false };

        public ListOfActivities()
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

        private void Img_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image send = (Image)sender;
            int id = 0;
            foreach (object item in ((Grid)send.Parent).Children)
            {
                switch (item.ToString())
                {
                    case "System.Windows.Controls.TextBlock" :
                        switch (((TextBlock)item).Name)
                        {
                            case "Name":
                                break;
                            case "Id" :
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
                    dbSingleton.deleteActivity(id);
                    break;
                case "Img_edit" :
                    NavigationService.Navigate(new Uri("/Views/EditOrNewActivity.xaml?id=" + id.ToString(), UriKind.Relative));
                    break;
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/EditOrNewActivity.xaml", UriKind.Relative));

            //if (pok.IsEnable)
            //    pok.IsEnable = false;
            //else
            //    pok.IsEnable = true;
        }
    }
}