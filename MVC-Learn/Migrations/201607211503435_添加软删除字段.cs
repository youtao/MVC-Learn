namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加软删除字段 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.SignalR_Connection", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.System_UserInfo", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.SignalR_Group", "Delete", c => c.Boolean(nullable: false));
            AddColumn("dbo.System_Menu", "Delete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.System_Menu", "Delete");
            DropColumn("dbo.SignalR_Group", "Delete");
            DropColumn("dbo.System_UserInfo", "Delete");
            DropColumn("dbo.SignalR_Connection", "Delete");
            DropColumn("dbo.Article", "Delete");
        }
    }
}
