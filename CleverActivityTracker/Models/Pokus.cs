using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverActivityTracker.Models
{
    class Pokus : INotifyPropertyBase 
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { this.SetProperty(ref this._isChecked, value); }
        }
    }
}
