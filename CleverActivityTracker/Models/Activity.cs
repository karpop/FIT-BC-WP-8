using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
   
    [Table]
    public class Activity : INotifyPropertyBase
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

        
        [Column]
        internal int? _folderId;

        // Entity reference, to identify the ToDoCategory "storage" table
        private EntityRef<Folder> _folderRef;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_folderRef", ThisKey = "_folderId", OtherKey = "Id", IsForeignKey = true)]
        public Folder FolderRef
        {
            get { return _folderRef.Entity; }
            set
            {
                OnPropertyChanging();
                _folderRef.Entity = value;
                if (value != null)
                {
                    value.ActivitiesRef.Add(this);
                    _folderId = value.Id;
                }
                OnPropertyChanged();
            }
        }

        // Define the entity set for the collection side of the relationship.
        private EntitySet<Schedule> _schedulesRef;

        [Association(Storage = "_schedulesRef", OtherKey = "_activityId", ThisKey = "Id")]
        public EntitySet<Schedule> SchedulesRef
        {
            get { return this._schedulesRef; }
            set { this._schedulesRef.Assign(value); }
        }

        // Assign handlers for the add and remove operations, respectively.
        public Activity()
        {
            _schedulesRef = new EntitySet<Schedule>(
                new Action<Schedule>(this.attach_Activity),
                new Action<Schedule>(this.detach_Activity)
                );
        }

        // Called during an add operation
        private void attach_Activity(Schedule schedule)
        {
            OnPropertyChanging();
            schedule.ActivityRef = this;
        }

        // Called during a remove operation
        private void detach_Activity(Schedule schedule)
        {
            OnPropertyChanging();
            schedule.ActivityRef = null;
        }
    }
}
