namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加部门表 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.System_Department",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.HierarchyId(),
                        CreateTime = c.DateTime(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.System_Department");
        }
    }
}
