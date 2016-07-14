namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改时间 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Article", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Article", "CreateTime", c => c.DateTime(nullable: false));
        }
    }
}
