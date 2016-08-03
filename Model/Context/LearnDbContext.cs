using System.Data.Entity;

namespace Model
{
    public class LearnDbContext : DbContext
    {
        public LearnDbContext() : base("LearnDbContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>() // UserInfo_Group 中间表
                .HasMany(e => e.UserInfos)
                .WithMany(e => e.Groups)
                .Map(e =>
                {
                    e.ToTable("SignalR_MT_UserInfo_Group");
                    e.MapLeftKey("UserInfo_Id");
                    e.MapRightKey("Group_Id");                    
                });
            base.OnModelCreating(modelBuilder);
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