namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加Menu模型 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Icon = c.String(),
                        ParentId = c.Long(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menu", "ParentId", "dbo.Menu");
            DropIndex("dbo.Menu", new[] { "ParentId" });
            DropTable("dbo.Menu");
        }
    }
}
