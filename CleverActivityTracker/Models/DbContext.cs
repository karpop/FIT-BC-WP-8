using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    [Database]
    public class MyDbContext : DataContext
    {
        public const string DBConnectionString = "isostore:/myDatabase.sdf";

        public Table<Folder> Folders;
        public Table<Activity> Activities;
        public Table<Schedule> Schedules;
        public Table<ActivityGroup> ActivityGroup;
        public Table<Group> Group;
        public Table<History> History;

        public MyDbContext(string connectionString = DBConnectionString) : base(connectionString) { }
    }
}
