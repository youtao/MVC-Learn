namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改字段名 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.System_Menu", "MenuOrder", c => c.Int(nullable: false));
            DropColumn("dbo.System_Menu", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.System_Menu", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.System_Menu", "MenuOrder");
        }
    }
}
