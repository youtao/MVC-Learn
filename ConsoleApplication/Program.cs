using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<LearnDbContext>(null);
            using (LearnDbContext db = new LearnDbContext())
            {
                db.UserInfo.ToList();                

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var sql = @"select Id,Title,Url,Icon,MenuOrder,ParentId,CreateTime,[Delete] from dbo.System_Menu";
                var query = db.Database.SqlQuery<Menu>(sql);
                var bySql = query.ToList();
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);                

                stopwatch.Restart();
                var byEf = db.Menu.ToList();
                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
                Console.ReadKey();
            }
        }
    }

    public class Menu
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
    }
}
