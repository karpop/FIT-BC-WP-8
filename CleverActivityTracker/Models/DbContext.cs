using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CleverActivityTracker.Models
{
    [Database]
    public class MyDbContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public const string DBConnectionString = "Data Source=isostore:/myDatabase.sdf";

        // Specify a table for the folders
        public Table<Folder> Folders;

        // Specify a table for the activitties.
        public Table<Activity> Activities;

        public Table<Schedule> Schedules;
        // Specify a table for the activitties.
       // public Table<Schedule> Sch;

        // Pass the connection string to the base class.
        public MyDbContext(string connectionString = DBConnectionString) : base(connectionString) { }
    }
}
