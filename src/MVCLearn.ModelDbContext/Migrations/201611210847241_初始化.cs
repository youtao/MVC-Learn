namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class 初始化 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SignalR_Connection",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ConnectionId = c.String(nullable: false),
                        UserAgent = c.String(),
                        Connected = c.Boolean(nullable: false),
                        UserInfoID = c.Long(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Privilege_UserInfo", t => t.UserInfoID, cascadeDelete: true)
                .Index(t => t.UserInfoID);

            CreateTable(
                "dbo.Privilege_UserInfo",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        NickName = c.String(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        SignoutTime = c.DateTime(),
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
                "dbo.Privilege_MenuInfo",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Icon = c.String(),
                        Order = c.Int(nullable: false),
                        ParentID = c.Long(),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Privilege_MenuInfo", t => t.ParentID)
                .Index(t => t.ParentID);

            CreateTable(
                "dbo.Privilege_Privilege",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PrivilegeName = c.String(),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Privilege_RoleInfo",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RoleName = c.String(),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SignalR_MT_UserInfo_Group",
                c => new
                    {
                        UserInfo_ID = c.Long(nullable: false),
                        Group_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfo_ID, t.Group_ID })
                .ForeignKey("dbo.SignalR_Group", t => t.UserInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.Privilege_UserInfo", t => t.Group_ID, cascadeDelete: true)
                .Index(t => t.UserInfo_ID)
                .Index(t => t.Group_ID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Privilege_MenuInfo", "ParentID", "dbo.Privilege_MenuInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_ID", "dbo.Privilege_UserInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "UserInfo_ID", "dbo.SignalR_Group");
            DropForeignKey("dbo.SignalR_Connection", "UserInfoID", "dbo.Privilege_UserInfo");
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "Group_ID" });
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "UserInfo_ID" });
            DropIndex("dbo.Privilege_MenuInfo", new[] { "ParentID" });
            DropIndex("dbo.SignalR_Connection", new[] { "UserInfoID" });
            DropTable("dbo.SignalR_MT_UserInfo_Group");
            DropTable("dbo.Privilege_RoleInfo");
            DropTable("dbo.Privilege_Privilege");
            DropTable("dbo.Privilege_MenuInfo");
            DropTable("dbo.SignalR_Group");
            DropTable("dbo.Privilege_UserInfo");
            DropTable("dbo.SignalR_Connection");
        }
    }
}
