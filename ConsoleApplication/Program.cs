using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Hierarchy;
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
                //var parent = HierarchyId.GetRoot();                
                //var level = parent.GetLevel();
                //var list = db.Department.Where(e => e.Path.IsDescendantOf(parent) && e.Path.GetLevel() == level).ToList();
                //for (int i = 0; i < 20; i++)
                //{
                //    var count = db.Department.Count() + 1;
                //    var last = db.Department.OrderByDescending(e => e.Path).FirstOrDefault(e => e.Path.GetLevel() == 1);
                //    var path = new HierarchyId("/1/");
                //    if (last != null)
                //    {
                //        path = HierarchyId.GetRoot().GetDescendant(last.Path, null);
                //    }
                //    Department department = new Department()
                //    {
                //        Name = "部门" + count,
                //        Path = path
                //    };
                //    db.Department.Add(department);                    
                //    db.SaveChanges();
                //}
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
