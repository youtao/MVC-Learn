using System.Data.Entity.Migrations;

namespace WebUI.Migrations
{
    public partial class 修改UserInfo字段名 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "LoginTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserInfo", "SignoutTime", c => c.DateTime());
            DropColumn("dbo.UserInfo", "LastLoginTime");
            DropColumn("dbo.UserInfo", "LastSignoutTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfo", "LastSignoutTime", c => c.DateTime());
            AddColumn("dbo.UserInfo", "LastLoginTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserInfo", "SignoutTime");
            DropColumn("dbo.UserInfo", "LoginTime");
        }
    }
}
