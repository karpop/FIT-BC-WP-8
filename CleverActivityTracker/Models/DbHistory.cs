using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    /// <summary>
    /// Trida pro zaznam tabulky historie aktivit
    /// </summary>
    [Table]
    public class History : INotifyPropertyBase
    {
        /// <summary>
        /// Primarni klic historie.
        /// </summary>
        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL IDENTITY", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set { this.SetProperty(ref this._id, value); }
        }

        /// <summary>
        /// Cas zacatku daneho zaznamu.
        /// </summary>
        private DateTime _from;
        [Column]
        public DateTime From
        {
            get { return _from; }
            set { this.SetProperty(ref this._from, value); }
        }

        /// <summary>
        /// Cas konce daneho zaznamu.
        /// </summary>
        private DateTime _to;
        [Column]
        public DateTime To
        {
            get { return _to; }
            set { this.SetProperty(ref this._to, value); }
        }

        /// <summary>
        /// Cizi klic obsahujici id aktivity.
        /// </summary>
        [Column]
        internal int? _activityId;

        /// <summary>
        /// Odkaz na aktivitu.
        /// </summary>
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
