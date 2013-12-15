using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    /// <summary>
    /// Trida pro zaznam tabulky aktivit
    /// </summary>
    [Table]
    public class Activity : INotifyPropertyBase
    {

        public Activity()
        {
            _schedulesRef = new EntitySet<Schedule>(
                new Action<Schedule>(this.addSchedulesRef),
                new Action<Schedule>(this.removeSchedulesRef)
                );

            _activitiesGroupsRef = new EntitySet<ActivityGroup>(
                new Action<ActivityGroup>(this.addActivitiesGroupsRef),
                new Action<ActivityGroup>(this.removeActivitiesGroupsRef)
                );

            _historiesRef = new EntitySet<History>(
                new Action<History>(this.addHistoriesRef),
                new Action<History>(this.removeHistoriesRef)
                );
        }

        /// <summary>
        /// Primarkni klic aktivity.
        /// </summary>
        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL IDENTITY", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set { this.SetProperty(ref this._id, value); }
        }

        /// <summary>
        /// Nazev aktivity.
        /// </summary>
        private string _name;
        [Column]
        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref this._name, value); }
        }

        /// <summary>
        /// Cizi klic obsahujici id slozky.
        /// </summary>
        /// <remarks>
        /// Muze nabyvat i hodnoty null.
        /// </remarks>
        [Column]
        internal int? _folderId;
        /// <summary>
        /// Odkaz na slozku v ktere se naleza aktivita.
        /// </summary>
        private EntityRef<Folder> _folderRef;
        [Association(Storage = "_folderRef", ThisKey = "_folderId", OtherKey = "Id", IsForeignKey = true)]
        public Folder FolderRef
        {
            get { return _folderRef.Entity; }
            set
            {
                OnPropertyChanging();
                _folderRef.Entity = value;
                if (value != null)
                    _folderId = value.Id;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// List vsech zaznamu v planu.
        /// </summary>
        private EntitySet<Schedule> _schedulesRef;
        [Association(Storage = "_schedulesRef", OtherKey = "_activityId", ThisKey = "Id")]
        public EntitySet<Schedule> SchedulesRef
        {
            get { return this._schedulesRef; }
            set { this._schedulesRef.Assign(value); }
        }

        /// <summary>
        /// List vsech skupin prirazenych k aktivite.
        /// </summary>
        /// <remarks>
        /// Odkaz je delany pres pomocnou tabulku.
        /// </remarks>
        private EntitySet<ActivityGroup> _activitiesGroupsRef;
        [Association(Storage = "_activitiesGroupsRef", OtherKey = "_activityId", ThisKey = "Id")]
        public EntitySet<ActivityGroup> ActivitiesGroupsRef
        {
            get { return this._activitiesGroupsRef; }
            set { this._activitiesGroupsRef.Assign(value); }
        }

        /// <summary>
        /// Odkaz na vsechny zaznam v histori.
        /// </summary>
        private EntitySet<History> _historiesRef;
        [Association(Storage = "_historiesRef", OtherKey = "_activityId", ThisKey = "Id")]
        public EntitySet<History> HistoriesRef
        {
            get { return this._historiesRef; }
            set { this._historiesRef.Assign(value); }
        }

        /// <summary>
        /// Funkce volana pri pridani do seznamu zaznamu v planu.
        /// </summary>
        /// <param name="schedule">
        /// Plan ktery se prirazuje k seznamu.
        /// </param>
        private void addSchedulesRef(Schedule schedule)
        {
            OnPropertyChanging();
            schedule.ActivityRef = this;
        }
        /// <summary>
        /// Funkce volana pri odebrani do seznamu zaznamu v planu.
        /// </summary>
        /// <param name="schedule">
        /// Plan ktery se odebira ze seznamu.
        /// </param>
        private void removeSchedulesRef(Schedule schedule)
        {
            OnPropertyChanging();
            schedule.ActivityRef = null;
        }

        /// <summary>
        /// Funkce volana pri pridani do seznamu skupin.
        /// </summary>
        /// <param name="schedule">
        /// Skupina, ktera se pridava k seznamu.
        /// </param>
        private void addActivitiesGroupsRef(ActivityGroup activityGroup)
        {
            OnPropertyChanging();
            activityGroup.ActivityRef = this;
        }
        /// <summary>
        /// Funkce volana pri odebrani ze seznamu skupin.
        /// </summary>
        /// <param name="schedule">
        /// Skupina, ktera se odebira ze seznamu.
        /// </param>
        private void removeActivitiesGroupsRef(ActivityGroup activityGroup)
        {
            OnPropertyChanging();
            activityGroup.ActivityRef = null;
        }

        /// <summary>
        /// Funkce volana pri pridani do seznamu historii.
        /// </summary>
        /// <param name="schedule">
        /// Historie, ktera se pridava do seznamu.
        /// </param>
        private void addHistoriesRef(History history)
        {
            OnPropertyChanging();
            history.ActivityRef = this;
        }
        /// <summary>
        /// Funkce volana pri odstraneni do seznamu historii.
        /// </summary>
        /// <param name="schedule">
        /// Historie, ktera se odstranuje ze seznamu.
        /// </param>
        private void removeHistoriesRef(History history)
        {
            OnPropertyChanging();
            history.ActivityRef = null;
        }
    }
}
