using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    [Table]
    public class ActivityGroup : INotifyPropertyBase
    {

        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL IDENTITY", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set { this.SetProperty(ref this._id, value); }
        }

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

        [Column]
        internal int? _groupId;
        private EntityRef<Group> _groupRef;
        [Association(Storage = "_groupRef", ThisKey = "_groupId", OtherKey = "Id", IsForeignKey = true)]
        public Group GroupRef
        {
            get { return _groupRef.Entity; }
            set
            {
                OnPropertyChanging();
                _groupRef.Entity = value;
                if (value != null)
                    _groupId = value.Id;
                OnPropertyChanged();
            }
        }
    }
}
