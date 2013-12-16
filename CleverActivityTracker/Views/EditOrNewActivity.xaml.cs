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

namespace CleverActivityTracker.Views
{
    public partial class EditOrNewActivity : PhoneApplicationPage
    {
        PageModel model = new PageModel();
        ViewModelDbSingleton db = ViewModelDbSingleton.Instance;

        public EditOrNewActivity()
        {
            InitializeComponent();
            model.EnableEdit = false;
            DataContext = model;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string str;
            if (NavigationContext.QueryString.TryGetValue("activityId", out str))
            {
                model.EnableEdit = false;
                model.idActivity = Convert.ToInt32(str);
                model.Activity = db.FindActivity((int)model.idActivity);
            }
            else
            {
                model.EnableEdit = true;
                model.idActivity = null;
                model.Activity = new Activity();
                model.Activity.Name = "";
                foreach (ApplicationBarIconButton item in ApplicationBar.Buttons)
                    if (item.Text == "Delete")
                        item.IsEnabled = false;
            }
        }

        private void ApplicationBarIconButton_Delete(object sender, EventArgs e)
        {
            if (model.idActivity != null)
                db.deleteActivity(model.Activity);
            NavigationService.GoBack();
        }

        private void ApplicationBarIconButton_Save(object sender, EventArgs e)
        {
            if (TextBox_ActivityName.Text.Trim() != "")
            {
                if (model.idActivity != null)
                {
                    model.Activity.Name = TextBox_ActivityName.Text;
                    db.SubmitChangesDb();
                }
                else
                    db.CreateActivity(TextBox_ActivityName.Text);
            }
            NavigationService.GoBack();
        }

        private void ApplicationBarIconButton_Edit(object sender, EventArgs e)
        {
            model.EnableEdit = model.EnableEdit == true ? false : true;
        }

        private class PageModel : INotifyPropertyBase
        {
            public int? idActivity = null;

            private Activity _activity;
            public Activity Activity
            {
                get { return _activity; }
                set { this.SetPropertyChanged(ref this._activity, value); }
            }

            private bool _enableEdit;
            public bool EnableEdit
            {
                get { return _enableEdit; }
                set { this.SetPropertyChanged(ref this._enableEdit, value); }
            }
        }
    }
}