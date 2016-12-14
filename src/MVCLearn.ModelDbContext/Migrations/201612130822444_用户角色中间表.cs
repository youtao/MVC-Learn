namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 用户角色中间表 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Privilege_MT_UserInfo_RoleInfo",
                c => new
                    {
                        UserInfo_ID = c.Int(nullable: false),
                        RoleInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfo_ID, t.RoleInfo_ID })
                .ForeignKey("dbo.System_UserInfo", t => t.UserInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.System_RoleInfo", t => t.RoleInfo_ID, cascadeDelete: true)
                .Index(t => t.UserInfo_ID)
                .Index(t => t.RoleInfo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Privilege_MT_UserInfo_RoleInfo", "RoleInfo_ID", "dbo.System_RoleInfo");
            DropForeignKey("dbo.Privilege_MT_UserInfo_RoleInfo", "UserInfo_ID", "dbo.System_UserInfo");
            DropIndex("dbo.Privilege_MT_UserInfo_RoleInfo", new[] { "RoleInfo_ID" });
            DropIndex("dbo.Privilege_MT_UserInfo_RoleInfo", new[] { "UserInfo_ID" });
            DropTable("dbo.Privilege_MT_UserInfo_RoleInfo");
        }
    }
}
