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
    }
}