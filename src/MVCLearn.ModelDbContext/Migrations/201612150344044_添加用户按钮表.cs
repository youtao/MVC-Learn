namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加用户按钮表 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.System_ButtonInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ButtonName = c.String(),
                        ButtonType = c.Int(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Privilege_MT_RoleInfo_ButtonInfo",
                c => new
                    {
                        RoleInfo_ID = c.Int(nullable: false),
                        ButtonInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleInfo_ID, t.ButtonInfo_ID })
                .ForeignKey("dbo.System_RoleInfo", t => t.RoleInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.System_ButtonInfo", t => t.ButtonInfo_ID, cascadeDelete: true)
                .Index(t => t.RoleInfo_ID)
                .Index(t => t.ButtonInfo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Privilege_MT_RoleInfo_ButtonInfo", "ButtonInfo_ID", "dbo.System_ButtonInfo");
            DropForeignKey("dbo.Privilege_MT_RoleInfo_ButtonInfo", "RoleInfo_ID", "dbo.System_RoleInfo");
            DropIndex("dbo.Privilege_MT_RoleInfo_ButtonInfo", new[] { "ButtonInfo_ID" });
            DropIndex("dbo.Privilege_MT_RoleInfo_ButtonInfo", new[] { "RoleInfo_ID" });
            DropTable("dbo.Privilege_MT_RoleInfo_ButtonInfo");
            DropTable("dbo.System_ButtonInfo");
        }
    }
}
