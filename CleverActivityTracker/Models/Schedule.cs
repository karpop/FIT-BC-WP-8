using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverActivityTracker.Models 
{
    [Table]
    public class Schedule : INotifyPropertyBase
    {

        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL IDENTITY", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private int? _order;
        [Column]
        public int? Order
        {
            get { return _order; }
            set { this.SetProperty(ref this._order, value); }
        }

        // Internal column for the associated
        [Column]
        internal int? _activityId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Activity> _activityRef;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_activityRef", ThisKey = "_activityId", OtherKey = "Id", IsForeignKey = true)]
        public Activity ActivityRef
        {
            get { return _activityRef.Entity; }
            set
            {
                OnPropertyChanging();
                _activityRef.Entity = value;
                if (value != null)
                {
                    value.SchedulesRef.Add(this);
                    _activityId = value.Id;
                }
                OnPropertyChanged();
            }
        }
    }
}
