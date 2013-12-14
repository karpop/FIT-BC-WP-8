using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    [Table]
    public class Folder : INotifyPropertyBase
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

        // Define the entity set for the collection side of the relationship.
        private EntitySet<Activity> _activitiesRef;

        [Association(Storage = "_activitiesRef", OtherKey = "_folderId", ThisKey = "Id")]
        public EntitySet<Activity> ActivitiesRef
        {
            get { return this._activitiesRef; }
            set { this._activitiesRef.Assign(value); }
        }

        // Assign handlers for the add and remove operations, respectively.
        public Folder()
        {
            _activitiesRef = new EntitySet<Activity>(
                new Action<Activity>(this.attach_Foder),
                new Action<Activity>(this.detach_Folder)
                );
        }

        // Called during an add operation
        private void attach_Foder(Activity activity)
        {
            OnPropertyChanging();
            activity.FolderRef = this;
        }

        // Called during a remove operation
        private void detach_Folder(Activity activity)
        {
            OnPropertyChanging();
            activity.FolderRef = null;
        }
    }
}
