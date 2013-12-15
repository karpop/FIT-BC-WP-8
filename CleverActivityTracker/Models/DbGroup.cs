using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    [Table]
    public class Group : INotifyPropertyBase
    {

        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL IDENTITY", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private string _name;
        [Column]
        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref this._name, value); }
        }

        private EntitySet<ActivityGroup> _activitiesGroupsRef;
        [Association(Storage = "_activitiesGroupsRef", OtherKey = "_groupId", ThisKey = "Id")]
        public EntitySet<ActivityGroup> ActivitiesGroupsRef
        {
            get { return this._activitiesGroupsRef; }
            set { this._activitiesGroupsRef.Assign(value); }
        }

        public Group()
        {
            _activitiesGroupsRef = new EntitySet<ActivityGroup>(
                new Action<ActivityGroup>(this.addActivitiesGroupsRef),
                new Action<ActivityGroup>(this.removeActivitiesGroupsRef)
                );
        }

        private void addActivitiesGroupsRef(ActivityGroup activityGroup)
        {
            OnPropertyChanging();
            activityGroup.GroupRef = this;
        }
        private void removeActivitiesGroupsRef(ActivityGroup activityGroup)
        {
            OnPropertyChanging();
            activityGroup.GroupRef = null;
        }
    }
}
