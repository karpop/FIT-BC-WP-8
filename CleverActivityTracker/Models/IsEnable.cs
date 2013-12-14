using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverActivityTracker.Models
{
    class NotifyEnabled : INotifyPropertyBase
    {
        public bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { this.SetProperty(ref this._isEnable, value); }
        }
    }
}
