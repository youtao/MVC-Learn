namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改Unique字段 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Unique", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Article", "Uinque");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Article", "Uinque", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.Article", "Unique");
        }
    }
}
