using System.Data.Entity;
using Domain.Entity;

namespace Infrastructure
{
    public class DDDLearnDbContext : DbContext
    {
        public DDDLearnDbContext() : base("DDDLearnDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOptional(e => e.ArticleExt)
                .WithRequired(e => e.Article);

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
        public virtual DbSet<ArticleExt> ArticleExt { get; set; }

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