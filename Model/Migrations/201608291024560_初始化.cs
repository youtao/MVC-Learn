namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 初始化 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Unique = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        Content = c.String(),
                        Description = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SignalR_Connection",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ConnectionId = c.String(nullable: false),
                        UserAgent = c.String(),
                        Connected = c.Boolean(nullable: false),
                        UserInfoId = c.Long(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_UserInfo", t => t.UserInfoId, cascadeDelete: true)
                .Index(t => t.UserInfoId);
            
            CreateTable(
                "dbo.System_UserInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        NickName = c.String(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        SignoutTime = c.DateTime(),
                        CreateTime = c.DateTime(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SignalR_Group",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System_Menu",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Url = c.String(),
                        Icon = c.String(),
                        MenuOrder = c.Int(nullable: false),
                        ParentId = c.Long(),
                        CreateTime = c.DateTime(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Menu", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.SignalR_MT_UserInfo_Group",
                c => new
                    {
                        UserInfo_Id = c.Long(nullable: false),
                        Group_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfo_Id, t.Group_Id })
                .ForeignKey("dbo.SignalR_Group", t => t.UserInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.System_UserInfo", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.UserInfo_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.System_Menu", "ParentId", "dbo.System_Menu");
            DropForeignKey("dbo.SignalR_Connection", "UserInfoId", "dbo.System_UserInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_Id", "dbo.System_UserInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "UserInfo_Id", "dbo.SignalR_Group");
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "Group_Id" });
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "UserInfo_Id" });
            DropIndex("dbo.System_Menu", new[] { "ParentId" });
            DropIndex("dbo.SignalR_Connection", new[] { "UserInfoId" });
            DropTable("dbo.SignalR_MT_UserInfo_Group");
            DropTable("dbo.System_Menu");
            DropTable("dbo.SignalR_Group");
            DropTable("dbo.System_UserInfo");
            DropTable("dbo.SignalR_Connection");
            DropTable("dbo.Article");
        }
    }
}
