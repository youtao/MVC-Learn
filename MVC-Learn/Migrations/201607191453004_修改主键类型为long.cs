namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改主键类型为long : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupUserInfoes", "UserInfo_Id", "dbo.UserInfo");
            DropForeignKey("dbo.SignalR_Connection", "UserId", "dbo.UserInfo");
            DropForeignKey("dbo.GroupUserInfoes", "Group_Id", "dbo.SignalR_Group");
            DropIndex("dbo.SignalR_Connection", new[] { "UserId" });
            DropIndex("dbo.GroupUserInfoes", new[] { "Group_Id" });
            DropIndex("dbo.GroupUserInfoes", new[] { "UserInfo_Id" });
            RenameColumn(table: "dbo.SignalR_Connection", name: "UserId", newName: "UserInfoId");
            DropPrimaryKey("dbo.Article");
            DropPrimaryKey("dbo.SignalR_Connection");
            DropPrimaryKey("dbo.UserInfo");
            DropPrimaryKey("dbo.SignalR_Group");
            DropPrimaryKey("dbo.GroupUserInfoes");
            AlterColumn("dbo.Article", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_Connection", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_Connection", "UserInfoId", c => c.Long(nullable: false));
            AlterColumn("dbo.UserInfo", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_Group", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.GroupUserInfoes", "Group_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.GroupUserInfoes", "UserInfo_Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Article", "Id");
            AddPrimaryKey("dbo.SignalR_Connection", "Id");
            AddPrimaryKey("dbo.UserInfo", "Id");
            AddPrimaryKey("dbo.SignalR_Group", "Id");
            AddPrimaryKey("dbo.GroupUserInfoes", new[] { "Group_Id", "UserInfo_Id" });
            CreateIndex("dbo.SignalR_Connection", "UserInfoId");
            CreateIndex("dbo.GroupUserInfoes", "Group_Id");
            CreateIndex("dbo.GroupUserInfoes", "UserInfo_Id");
            AddForeignKey("dbo.GroupUserInfoes", "UserInfo_Id", "dbo.UserInfo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SignalR_Connection", "UserInfoId", "dbo.UserInfo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupUserInfoes", "Group_Id", "dbo.SignalR_Group", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupUserInfoes", "Group_Id", "dbo.SignalR_Group");
            DropForeignKey("dbo.SignalR_Connection", "UserInfoId", "dbo.UserInfo");
            DropForeignKey("dbo.GroupUserInfoes", "UserInfo_Id", "dbo.UserInfo");
            DropIndex("dbo.GroupUserInfoes", new[] { "UserInfo_Id" });
            DropIndex("dbo.GroupUserInfoes", new[] { "Group_Id" });
            DropIndex("dbo.SignalR_Connection", new[] { "UserInfoId" });
            DropPrimaryKey("dbo.GroupUserInfoes");
            DropPrimaryKey("dbo.SignalR_Group");
            DropPrimaryKey("dbo.UserInfo");
            DropPrimaryKey("dbo.SignalR_Connection");
            DropPrimaryKey("dbo.Article");
            AlterColumn("dbo.GroupUserInfoes", "UserInfo_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.GroupUserInfoes", "Group_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.SignalR_Group", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserInfo", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_Connection", "UserInfoId", c => c.Int(nullable: false));
            AlterColumn("dbo.SignalR_Connection", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Article", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.GroupUserInfoes", new[] { "Group_Id", "UserInfo_Id" });
            AddPrimaryKey("dbo.SignalR_Group", "Id");
            AddPrimaryKey("dbo.UserInfo", "Id");
            AddPrimaryKey("dbo.SignalR_Connection", "Id");
            AddPrimaryKey("dbo.Article", "Id");
            RenameColumn(table: "dbo.SignalR_Connection", name: "UserInfoId", newName: "UserId");
            CreateIndex("dbo.GroupUserInfoes", "UserInfo_Id");
            CreateIndex("dbo.GroupUserInfoes", "Group_Id");
            CreateIndex("dbo.SignalR_Connection", "UserId");
            AddForeignKey("dbo.GroupUserInfoes", "Group_Id", "dbo.SignalR_Group", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SignalR_Connection", "UserId", "dbo.UserInfo", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupUserInfoes", "UserInfo_Id", "dbo.UserInfo", "Id", cascadeDelete: true);
        }
    }
}
