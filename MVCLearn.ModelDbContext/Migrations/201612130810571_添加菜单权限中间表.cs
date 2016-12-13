namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加菜单权限中间表 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Privilege_MT_RoleInfo_MenuInfo",
                c => new
                    {
                        RoleInfo_ID = c.Int(nullable: false),
                        MenuInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleInfo_ID, t.MenuInfo_ID })
                .ForeignKey("dbo.Privilege_RoleInfo", t => t.RoleInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.Privilege_MenuInfo", t => t.MenuInfo_ID, cascadeDelete: true)
                .Index(t => t.RoleInfo_ID)
                .Index(t => t.MenuInfo_ID);
            
            AddColumn("dbo.Privilege_MenuInfo", "IsIframe", c => c.Boolean(nullable: false));
            AddColumn("dbo.Privilege_MenuInfo", "IsMenu", c => c.Boolean(nullable: false));
            AddColumn("dbo.Privilege_MenuInfo", "IsPublick", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Privilege_MT_RoleInfo_MenuInfo", "MenuInfo_ID", "dbo.Privilege_MenuInfo");
            DropForeignKey("dbo.Privilege_MT_RoleInfo_MenuInfo", "RoleInfo_ID", "dbo.Privilege_RoleInfo");
            DropIndex("dbo.Privilege_MT_RoleInfo_MenuInfo", new[] { "MenuInfo_ID" });
            DropIndex("dbo.Privilege_MT_RoleInfo_MenuInfo", new[] { "RoleInfo_ID" });
            DropColumn("dbo.Privilege_MenuInfo", "IsPublick");
            DropColumn("dbo.Privilege_MenuInfo", "IsMenu");
            DropColumn("dbo.Privilege_MenuInfo", "IsIframe");
            DropTable("dbo.Privilege_MT_RoleInfo_MenuInfo");
        }
    }
}
