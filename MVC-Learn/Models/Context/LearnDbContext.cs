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
        #region Article

        /// <summary>
        /// 文章
        /// </summary>
        public virtual DbSet<Article> Article { get; set; }

        #endregion

        #region Menu

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual DbSet<Menu> Menu { get; set; }

        #endregion

        #region User

        /// <summary>
        /// SignalR-连接
        /// </summary>
        public virtual DbSet<Connection> Connection { get; set; }
        /// <summary>
        /// SignalR-组
        /// </summary>
        public virtual DbSet<Group> Group { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        #endregion
        
    }
}