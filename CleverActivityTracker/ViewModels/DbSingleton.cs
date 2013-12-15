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

        private ObservableCollection<ActivityGroup> _allActivityGroupItems;
        public ObservableCollection<ActivityGroup> AllActivityGroupItems
        {
            get { return _allActivityGroupItems; }
            set { this.SetPropertyChanged(ref _allActivityGroupItems, value); }
        }

        private ObservableCollection<History> _allHistoryItems;
        public ObservableCollection<History> AllHistoryItems
        {
            get { return _allHistoryItems; }
            set { this.SetPropertyChanged(ref _allHistoryItems, value); }
        }

        private ObservableCollection<Group> _allGroupItems;
        public ObservableCollection<Group> AllGroupItems
        {
            get { return _allGroupItems; }
            set { this.SetPropertyChanged(ref _allGroupItems, value); }
        }

        private ObservableCollection<Activity> _allActivityItems;
        public ObservableCollection<Activity> AllActivityItems
        {
            get { return _allActivityItems; }
            set { this.SetPropertyChanged(ref _allActivityItems, value); }
        }

        private ObservableCollection<Schedule> _allScheduleItems;
        public ObservableCollection<Schedule> AllScheduleItems
        {
            get { return _allScheduleItems; }
            set { this.SetPropertyChanged(ref _allScheduleItems, value); }
        }

        private ObservableCollection<Folder> _allFolderItems;
        public ObservableCollection<Folder> AllFolderItems
        {
            get { return _allFolderItems; }
            set { this.SetPropertyChanged(ref _allFolderItems, value); }
        }

        public ViewModelDbSingleton() 
        {
            if (!db.DatabaseExists()) { db.CreateDatabase(); }
            AllActivityItems = new ObservableCollection<Activity>(db.Activities);
            AllFolderItems = new ObservableCollection<Folder>(db.Folders);
            AllScheduleItems = new ObservableCollection<Schedule>(db.Schedules.OrderBy(x => x.Order));
            AllGroupItems = new ObservableCollection<Group>(db.Group);
            AllHistoryItems = new ObservableCollection<History>(db.History);
            AllActivityGroupItems = new ObservableCollection<ActivityGroup>(db.ActivityGroup);
        }

        ~ViewModelDbSingleton()
        {
            SaveOrderSchedule();
            db.SubmitChanges();
        }

        public void ClearDb() 
        {
            foreach (Group group in db.Group) { DeleteGroup(group); }
            foreach (Activity activity in db.Activities) { deleteActivity(activity); }
            foreach (Folder folder in db.Folders) { DeleteFolder(folder); }
            foreach (Schedule schedule in db.Schedules) { deleteSchedule(schedule); }
        }

        public void SubmitChangesDb() { db.SubmitChanges(); }

        public void CreateSchedule(Activity activity)
        {
            int order = 1;
            if (db.Schedules.Count() != 0) { order = db.Schedules.Max(x => x.Order) + 1; };    
            Schedule schedule = new Schedule { Order = order };
            db.Schedules.InsertOnSubmit(schedule);
            activity.SchedulesRef.Add(schedule);
            db.SubmitChanges();
            AllScheduleItems.Add(schedule);
        }

        public void deleteSchedule(Schedule schedule)
        {
            if (schedule.ActivityRef != null) { schedule.ActivityRef.SchedulesRef.Remove(schedule); }
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
            Activity activity = new Activity { Name = name };
            db.Activities.InsertOnSubmit(activity);
            if (folder != null) { folder.ActivitiesRef.Add(activity); }
            db.SubmitChanges();
            AllActivityItems.Add(activity);
        }

        public void deleteActivity(Activity activity)
        {
            //foreach (Schedule item in activity.SchedulesRef) { deleteSchedule(item); };
            if (activity.FolderRef != null) { activity.FolderRef.ActivitiesRef.Remove(activity); }
            AllActivityItems.Remove(activity);
            db.Activities.DeleteOnSubmit(activity);
            db.SubmitChanges();      
        }

        public Activity FindActivity(int id)
        {
            return db.Activities.Where(x => x.Id == id).First();
        }

        public void CreateFolder(string name, Folder parentFolder = null)
        {
            Folder folder = new Folder { Name = name };
            db.Folders.InsertOnSubmit(folder);
            if (parentFolder != null) { parentFolder.SubFoldersRef.Add(folder); }
            db.SubmitChanges();
            AllFolderItems.Add(folder);
        }

        public void DeleteFolder(Folder folder)
        {
            //foreach (Activity item in folder.ActivitiesRef) { deleteActivity(item); };
            if (folder.ParentFolderRef != null) { folder.ParentFolderRef.SubFoldersRef.Remove(folder); }
            AllFolderItems.Remove(folder);
            db.Folders.DeleteOnSubmit(folder);
            db.SubmitChanges();
        }

        public Folder FindFolder(int id)
        {
            return db.Folders.Where(x => x.Id == id).First();
        }

        public void CreateActivityGroup(Activity activity, Group group)
        {
            ActivityGroup activityGroup = new ActivityGroup();
            db.ActivityGroup.InsertOnSubmit(activityGroup);
            activity.ActivitiesGroupsRef.Add(activityGroup);
            group.ActivitiesGroupsRef.Add(activityGroup);
            db.SubmitChanges();
            AllActivityGroupItems.Add(activityGroup);
        }

        public void DeleteActivityGroup(ActivityGroup activityGroup)
        {
            activityGroup.ActivityRef.ActivitiesGroupsRef.Remove(activityGroup);
            activityGroup.GroupRef.ActivitiesGroupsRef.Remove(activityGroup);
            AllActivityGroupItems.Remove(activityGroup);
            db.ActivityGroup.DeleteOnSubmit(activityGroup);
            db.SubmitChanges();
        }


        public void CreateGroup(string name)
        {
            Group group = new Group { Name = name };
            db.Group.InsertOnSubmit(group);
            db.SubmitChanges();
            AllGroupItems.Add(group);
        }

        public void DeleteGroup(Group group)
        {
            AllGroupItems.Remove(group);
            db.Group.DeleteOnSubmit(group);
            db.SubmitChanges();
        }

        public void CreateHistory(Activity activity)
        {
            History history = new History();
            activity.HistoriesRef.Add(history);
            db.History.InsertOnSubmit(history);
            db.SubmitChanges();
            AllHistoryItems.Add(history);
        }

        public void deleteHistory(History history)
        {
            if (history.ActivityRef != null) { history.ActivityRef.HistoriesRef.Remove(history); }
            AllHistoryItems.Remove(history);
            db.History.DeleteOnSubmit(history);
            db.SubmitChanges();
        }
    }
}
