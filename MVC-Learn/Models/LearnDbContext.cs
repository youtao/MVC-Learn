using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Learn.Models
{
    public class LearnDbContext : DbContext
    {
        public LearnDbContext() : base("LearnDbContext")
        {

        }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Connection> Connection { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
    }
}