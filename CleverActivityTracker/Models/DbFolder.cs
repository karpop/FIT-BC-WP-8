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

        [Column]
        internal int? _parentFolderId;
        private EntityRef<Folder> _parentFolderRef;
        [Association(Storage = "_parentFolderRef", ThisKey = "_parentFolderId", OtherKey = "Id", IsForeignKey = true)]
        public Folder ParentFolderRef
        {
            get { return _parentFolderRef.Entity; }
            set
            {
                OnPropertyChanging();
                _parentFolderRef.Entity = value;
                if (value != null)
                    _parentFolderId = value.Id;
                OnPropertyChanged();
            }
        }

        // Define the entity set for the collection side of the relationship.
        private EntitySet<Folder> _subFoldersRef;
        [Association(Storage = "_subFoldersRef", OtherKey = "_parentFolderId", ThisKey = "Id")]
        public EntitySet<Folder> SubFoldersRef
        {
            get { return this._subFoldersRef; }
            set { this._subFoldersRef.Assign(value); }
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
                new Action<Activity>(this.addActivitiesRef),
                new Action<Activity>(this.removeActivitiesRef)
                );

            _subFoldersRef = new EntitySet<Folder>(
                new Action<Folder>(this.addSubFoldersRef),
                new Action<Folder>(this.removeSubFoldersRef)
                );
        }

        private void addActivitiesRef(Activity activity)
        {
            OnPropertyChanging();
            activity.FolderRef = this;
        }
        private void removeActivitiesRef(Activity activity)
        {
            OnPropertyChanging();
            activity.FolderRef = null;
        }


        private void addSubFoldersRef(Folder folder)
        {
            OnPropertyChanging();
            folder.ParentFolderRef = this;
        }
        private void removeSubFoldersRef(Folder folder)
        {
            OnPropertyChanging();
            folder.ParentFolderRef = null;
        }
    }
}
