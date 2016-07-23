using System.Data.Entity.Migrations;

namespace WebUI.Migrations
{
    public partial class 添加SignalR相关 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Article", "Index_Unique");
            CreateTable(
                "dbo.SignalR_Connection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionId = c.String(nullable: false),
                        UserAgent = c.String(),
                        Connected = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfo", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        NickName = c.String(nullable: false),
                        LastLoginTime = c.DateTime(nullable: false),
                        LastSignoutTime = c.DateTime(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SignalR_Group",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupUserInfoes",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        UserInfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.UserInfo_Id })
                .ForeignKey("dbo.SignalR_Group", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserInfo", t => t.UserInfo_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.UserInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignalR_Connection", "UserId", "dbo.UserInfo");
            DropForeignKey("dbo.GroupUserInfoes", "UserInfo_Id", "dbo.UserInfo");
            DropForeignKey("dbo.GroupUserInfoes", "Group_Id", "dbo.SignalR_Group");
            DropIndex("dbo.GroupUserInfoes", new[] { "UserInfo_Id" });
            DropIndex("dbo.GroupUserInfoes", new[] { "Group_Id" });
            DropIndex("dbo.SignalR_Connection", new[] { "UserId" });
            DropTable("dbo.GroupUserInfoes");
            DropTable("dbo.SignalR_Group");
            DropTable("dbo.UserInfo");
            DropTable("dbo.SignalR_Connection");
            CreateIndex("dbo.Article", "Unique", unique: true, name: "Index_Unique");
        }
    }
}
