using System.Data.Entity;
using MVCLearn.Model;

namespace MVCLearn.ModelDbContext
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
                .WithMany()
                .Map(e =>
                {
                    e.ToTable("SignalR_MT_UserInfo_Group");
                    e.MapLeftKey("UserInfo_ID");
                    e.MapRightKey("Group_ID");
                });
            base.OnModelCreating(modelBuilder);
        }

        #region Privilege

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual DbSet<MenuInfo> MenuInfo { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public virtual DbSet<Privilege> Privilege { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        #endregion

        #region SignalR

        /// <summary>
        /// SignalR-连接
        /// </summary>
        public virtual DbSet<Connection> Connection { get; set; }

        /// <summary>
        /// SignalR-组
        /// </summary>
        public virtual DbSet<Group> Group { get; set; }

        #endregion

    }
}