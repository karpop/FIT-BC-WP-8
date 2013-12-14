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
    public partial class EditOrNewActivity : PhoneApplicationPage
    {
        private EditOrNewActivityModel model = new EditOrNewActivityModel();
        private ViewModelDbSingleton db = ViewModelDbSingleton.Instance;
        private string id = null;

        public EditOrNewActivity()
        {
            InitializeComponent();
            DataContext = model;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);;
            if (NavigationContext.QueryString.TryGetValue("id", out id))
            {
                model.EnableEdit = false;
                model.idActivity = Convert.ToInt32(id);
                model.Activity = db.findActivity((int)model.idActivity);
                //delete.Click -= ApplicationBarIconButton_Click;
                //delete.Click += ApplicationBarIconButton_Click2;
            }
            else
            {
                model.EnableEdit = true;
                model.idActivity = null;
                model.Activity = new Activity();
                model.Activity.Name = "";
            }

           // string parameterValue1 = NavigationContext.QueryString["parameter1"];
           // string parameterValue2 = NavigationContext.QueryString["parameter2"];   
        }

        private void ApplicationBarIconButton_Save(object sender, EventArgs e)
        {
            if (Activity_Name.Text == "")
            {
                NavigationService.GoBack();
                return;
            }
            model.Activity.Name = Activity_Name.Text;
            if (model.idActivity == null)
                db.CreateActivity(Activity_Name.Text);
            else
                db.SubmitChanges();
            NavigationService.GoBack();      
        }

        private void ApplicationBarIconButton_Edit(object sender, EventArgs e)
        {
            model.EnableEdit = model.EnableEdit == true ? false : true;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if(id != null)
                ViewModelDbSingleton.Instance.deleteActivity(Convert.ToInt32(id));
            NavigationService.GoBack();  
        }

    }
}