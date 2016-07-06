namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unique添加索引 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Article", "Unique", unique: true, name: "Index_Unique");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Article", "Index_Unique");
        }
    }
}
