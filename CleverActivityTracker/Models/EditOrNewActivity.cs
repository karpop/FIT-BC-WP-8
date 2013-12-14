using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverActivityTracker.Models
{
    public class EditOrNewActivityModel : INotifyPropertyBase
    {
        public int? idActivity;

        private Activity _activity;
        public Activity Activity
        {
            get { return _activity; }
            set { this.SetProperty(ref this._activity, value); }
        }

        private bool _enableEdit;
        public bool EnableEdit
        { 
            get { return _enableEdit; } 
            set { this.SetProperty(ref this._enableEdit, value); } 
        }
    }
}
