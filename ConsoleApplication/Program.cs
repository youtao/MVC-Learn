using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (LearnDbContext db = new LearnDbContext())
            {
                db.UserInfo.Select(e => new
                {
                    Length = e.NickName.Length
                }).ToList();
            }
        }
    }
}
