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

        private ObservableCollection<Activity> _allActivityItems;
        public ObservableCollection<Activity> AllActivityItems
        {
            get { return _allActivityItems; }
            set { this.SetProperty(ref _allActivityItems, value); }
        }

        private ObservableCollection<Schedule> _allScheduleItems;
        public ObservableCollection<Schedule> AllScheduleItems
        {
            get { return _allScheduleItems; }
            set { this.SetProperty(ref _allScheduleItems, value); }
        }

        private ObservableCollection<Folder> _allFolderItems;
        public ObservableCollection<Folder> AllFolderItems
        {
            get { return _allFolderItems; }
            set { this.SetProperty(ref _allFolderItems, value); }
        }

        public ViewModelDbSingleton() 
        {
            if (!this.db.DatabaseExists()) { this.db.CreateDatabase(); };
            this.AllActivityItems = new ObservableCollection<Activity>(db.Activities);
            this.AllFolderItems = new ObservableCollection<Folder>(db.Folders);
            this.AllScheduleItems = new ObservableCollection<Schedule>(db.Schedules.OrderBy(x => x.Order));
        }

        ~ViewModelDbSingleton()
        {
            SaveOrderSchedule();
            db.SubmitChanges();
        }
        
        public void DeleteDb() { db.DeleteDatabase(); }

        public void SubmitChangesDb() { db.SubmitChanges(); }

        public void CreateSchedule(Activity activity)
        {
            int order = 1;
            if (db.Schedules.Count() != 0) { order = db.Schedules.Max(x => x.Order) + 1; };    
            Schedule schedule = new Schedule { ActivityRef = activity, Order = order };
            db.Schedules.InsertOnSubmit(schedule);
            db.SubmitChanges();
            AllScheduleItems.Add(schedule);
        }

        public void deleteSchedule(Schedule schedule)
        {
            AllScheduleItems.Remove(schedule);
            db.Schedules.DeleteOnSubmit(schedule);
            db.SubmitChanges();
        }

        public void SaveOrderSchedule()
        {
            int i = 1;
            foreach (Schedule schedule in AllScheduleItems) { schedule.Order = i++; };
        }

        public Schedule FindSchedule(int id)
        {
            return db.Schedules.Where(x => x.Id == id).First();
        }

        public void CreateActivity(string name, Folder folder = null)
        {
            Activity activity = new Activity { Name = name, FolderRef = folder };
            db.Activities.InsertOnSubmit(activity);
            db.SubmitChanges();
            AllActivityItems.Add(activity);
        }

        public void deleteActivity(Activity activity)
        {
            foreach (Schedule item in activity.SchedulesRef) { deleteSchedule(item); };
            AllActivityItems.Remove(activity);
            db.Activities.DeleteOnSubmit(activity);
            db.SubmitChanges();      
        }

        public Activity FindActivity(int id)
        {
            return db.Activities.Where(x => x.Id == id).First();
        }

        public void CreateFolder(string name)
        {
            Folder folder = new Folder { Name = name };
            db.Folders.InsertOnSubmit(folder);
            db.SubmitChanges();
            AllFolderItems.Add(folder);
        }

        public void DeleteFolder(Folder folder)
        {
            foreach (Activity item in folder.ActivitiesRef) { deleteActivity(item); };
            AllFolderItems.Remove(folder);
            db.Folders.DeleteOnSubmit(folder);
            db.SubmitChanges();
        }

        public Folder FindFolder(int id)
        {
            return db.Folders.Where(x => x.Id == id).First();
        }
    }
}
