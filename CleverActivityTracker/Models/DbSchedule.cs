using System.Data.Linq;
using System.Data.Linq.Mapping;

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

        private int _order;
        [Column]
        public int Order
        {
            get { return _order; }
            set { this.SetProperty(ref this._order, value); }
        }

        // Internal column for the associated
        [Column]
        internal int? _activityId;
        private EntityRef<Activity> _activityRef;
        [Association(Storage = "_activityRef", ThisKey = "_activityId", OtherKey = "Id", IsForeignKey = true)]
        public Activity ActivityRef
        {
            get { return _activityRef.Entity; }
            set
            {
                OnPropertyChanging();
                _activityRef.Entity = value;
                if (value != null)
                    _activityId = value.Id;
                OnPropertyChanged();
            }
        }
    }
}
