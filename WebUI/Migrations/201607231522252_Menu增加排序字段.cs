namespace WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu增加排序字段 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.System_Menu", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.System_Menu", "Order");
        }
    }
}
