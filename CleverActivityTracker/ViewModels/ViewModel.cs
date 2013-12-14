using CleverActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverActivityTracker.ViewModels 
{
    public sealed class ViewModelDbSingleton : INotifyPropertyBase
    {
        private static readonly ViewModelDbSingleton instance = new ViewModelDbSingleton();
        private readonly MyDbContext db = new MyDbContext();

        public static ViewModelDbSingleton Instance
        {
            get { return instance; }
        }

        private ObservableCollection<Activity> _allActivityItems = new ObservableCollection<Activity>();
        public ObservableCollection<Activity> AllActivityItems
        {
            get { return _allActivityItems; }
            set { this.SetProperty(ref this._allActivityItems, value); }
        }

        private ObservableCollection<Schedule> _allScheduleItems = new ObservableCollection<Schedule>();
        public ObservableCollection<Schedule> AllScheduleItems
        {
            get { return _allScheduleItems; }
            set { this.SetProperty(ref this._allScheduleItems, value); }
        }

        private ObservableCollection<Folder> _allFolderItems = new ObservableCollection<Folder>();
        public ObservableCollection<Folder> AllFolderItems
        {
            get { return _allFolderItems; }
            set { this.SetProperty(ref this._allFolderItems, value); }
        }

        private ViewModelDbSingleton() { }

        public void createDb()
        {
            if(!this.db.DatabaseExists())
                this.db.CreateDatabase();

            foreach (Schedule item in this.db.Schedules.OrderBy(x => x.Order))
                this.AllScheduleItems.Add(item);

            foreach (Activity item in this.db.Activities)
                this.AllActivityItems.Add(item);

            foreach (Folder item in db.Folders)
                this.AllFolderItems.Add(item);

            //AllActivityItems.OrderBy(x => x.Name);
        }


        public void deleteSchedule(int id)
        {
            var query =
                from schdule in db.Schedules
                where schdule.Id == id
                select schdule;

            AllScheduleItems.Remove(query.First());
            db.Schedules.DeleteOnSubmit(query.First());
            db.SubmitChanges();
        }

        public void deleteDb()
        {
            this.db.DeleteDatabase();
        }

        public void CreateFolder(string name)
        {
            Folder folder = new Folder { Name = name };

            db.Folders.InsertOnSubmit(folder);
            db.SubmitChanges();
        }

        public void CreateActivity(string name, Folder folder = null)
        {
            Activity activity = new Activity { Name = name, FolderRef = folder };

            db.Activities.InsertOnSubmit(activity);
            db.SubmitChanges();

            AllActivityItems.Add(activity);
        }

        public void CreateSchedule(Activity activity)
        {
            Schedule schedule = new Schedule { ActivityRef = activity };

            db.Schedules.InsertOnSubmit(schedule);
            db.SubmitChanges();

            AllScheduleItems.Add(schedule);
        }

        public void deleteActivity(int id)
        {
            var deleteActivity =
                from activity in db.Activities
                where activity.Id == id
                select activity;

            foreach (Schedule item in deleteActivity.First().SchedulesRef)
            {
                AllScheduleItems.Remove(item);
                this.deleteSchedule(item.Id);
            }

            AllActivityItems.Remove(deleteActivity.First());

            db.Activities.DeleteOnSubmit(deleteActivity.First());
            db.SubmitChanges();
        }

        public void updateActivity(Activity activityInput)
        {
            var query =
                from activity in db.Activities
                where activity.Id == activityInput.Id
                select activity;

            foreach (Activity item in query)
            {
                item.Name = activityInput.Name;
            }

            db.SubmitChanges();
        }

        public void SubmitChanges()
        {
            db.SubmitChanges();
        }

        public Activity findActivity(int id)
        {
            var findActivity =
                from activity in db.Activities
                where activity.Id == id
                select activity;

            return findActivity.First();
        }

        public Folder findFolder(int id)
        {
            var findFolder =
                from folder in db.Folders
                where folder.Id == id
                select folder;

            return findFolder.First();
        }
    }
}
