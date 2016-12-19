namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 初始化 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.System_AccessInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        IsPublick = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.SignalR_Connection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConnectionId = c.String(nullable: false),
                        UserAgent = c.String(),
                        Connected = c.Boolean(nullable: false),
                        UserInfoID = c.Int(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.System_UserInfo", t => t.UserInfoID, cascadeDelete: true)
                .Index(t => t.UserInfoID);
            
            CreateTable(
                "dbo.System_UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        NickName = c.String(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        SignoutTime = c.DateTime(),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.System_RoleInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SignalR_Group",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Privilege_MT_RoleInfo_AccessInfo",
                c => new
                    {
                        RoleInfo_ID = c.Int(nullable: false),
                        AccessInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleInfo_ID, t.AccessInfo_ID })
                .ForeignKey("dbo.System_RoleInfo", t => t.RoleInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.System_AccessInfo", t => t.AccessInfo_ID, cascadeDelete: true)
                .Index(t => t.RoleInfo_ID)
                .Index(t => t.AccessInfo_ID);
            
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
            
            CreateTable(
                "dbo.SignalR_MT_UserInfo_Group",
                c => new
                    {
                        UserInfo_ID = c.Long(nullable: false),
                        Group_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfo_ID, t.Group_ID })
                .ForeignKey("dbo.SignalR_Group", t => t.UserInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.System_UserInfo", t => t.Group_ID, cascadeDelete: true)
                .Index(t => t.UserInfo_ID)
                .Index(t => t.Group_ID);
            
            CreateTable(
                "dbo.System_MenuInfo",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Icon = c.String(),
                        Order = c.Int(nullable: false),
                        IsIframe = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.System_AccessInfo", t => t.ID)
                .ForeignKey("dbo.System_MenuInfo", t => t.ParentID)
                .Index(t => t.ID)
                .Index(t => t.ParentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.System_MenuInfo", "ParentID", "dbo.System_MenuInfo");
            DropForeignKey("dbo.System_MenuInfo", "ID", "dbo.System_AccessInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_ID", "dbo.System_UserInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "UserInfo_ID", "dbo.SignalR_Group");
            DropForeignKey("dbo.SignalR_Connection", "UserInfoID", "dbo.System_UserInfo");
            DropForeignKey("dbo.Privilege_MT_UserInfo_RoleInfo", "RoleInfo_ID", "dbo.System_RoleInfo");
            DropForeignKey("dbo.Privilege_MT_UserInfo_RoleInfo", "UserInfo_ID", "dbo.System_UserInfo");
            DropForeignKey("dbo.Privilege_MT_RoleInfo_ButtonInfo", "ButtonInfo_ID", "dbo.System_ButtonInfo");
            DropForeignKey("dbo.Privilege_MT_RoleInfo_ButtonInfo", "RoleInfo_ID", "dbo.System_RoleInfo");
            DropForeignKey("dbo.Privilege_MT_RoleInfo_AccessInfo", "AccessInfo_ID", "dbo.System_AccessInfo");
            DropForeignKey("dbo.Privilege_MT_RoleInfo_AccessInfo", "RoleInfo_ID", "dbo.System_RoleInfo");
            DropIndex("dbo.System_MenuInfo", new[] { "ParentID" });
            DropIndex("dbo.System_MenuInfo", new[] { "ID" });
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "Group_ID" });
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "UserInfo_ID" });
            DropIndex("dbo.Privilege_MT_UserInfo_RoleInfo", new[] { "RoleInfo_ID" });
            DropIndex("dbo.Privilege_MT_UserInfo_RoleInfo", new[] { "UserInfo_ID" });
            DropIndex("dbo.Privilege_MT_RoleInfo_ButtonInfo", new[] { "ButtonInfo_ID" });
            DropIndex("dbo.Privilege_MT_RoleInfo_ButtonInfo", new[] { "RoleInfo_ID" });
            DropIndex("dbo.Privilege_MT_RoleInfo_AccessInfo", new[] { "AccessInfo_ID" });
            DropIndex("dbo.Privilege_MT_RoleInfo_AccessInfo", new[] { "RoleInfo_ID" });
            DropIndex("dbo.SignalR_Connection", new[] { "UserInfoID" });
            DropTable("dbo.System_MenuInfo");
            DropTable("dbo.SignalR_MT_UserInfo_Group");
            DropTable("dbo.Privilege_MT_UserInfo_RoleInfo");
            DropTable("dbo.Privilege_MT_RoleInfo_ButtonInfo");
            DropTable("dbo.Privilege_MT_RoleInfo_AccessInfo");
            DropTable("dbo.SignalR_Group");
            DropTable("dbo.System_RoleInfo");
            DropTable("dbo.System_UserInfo");
            DropTable("dbo.SignalR_Connection");
            DropTable("dbo.System_ButtonInfo");
            DropTable("dbo.System_AccessInfo");
        }
    }
}
