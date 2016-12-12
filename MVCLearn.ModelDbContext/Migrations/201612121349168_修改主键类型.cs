namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改主键类型 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SignalR_Connection", "UserInfoID", "dbo.Privilege_UserInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_ID", "dbo.Privilege_UserInfo");
            DropForeignKey("dbo.Privilege_MenuInfo", "ParentID", "dbo.Privilege_MenuInfo");
            DropIndex("dbo.SignalR_Connection", new[] { "UserInfoID" });
            DropIndex("dbo.Privilege_MenuInfo", new[] { "ParentID" });
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "Group_ID" });
            DropPrimaryKey("dbo.SignalR_Connection");
            DropPrimaryKey("dbo.Privilege_UserInfo");
            DropPrimaryKey("dbo.Privilege_MenuInfo");
            DropPrimaryKey("dbo.Privilege_Privilege");
            DropPrimaryKey("dbo.Privilege_RoleInfo");
            DropPrimaryKey("dbo.SignalR_MT_UserInfo_Group");
            AlterColumn("dbo.SignalR_Connection", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_Connection", "UserInfoID", c => c.Int(nullable: false));
            AlterColumn("dbo.Privilege_UserInfo", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Privilege_MenuInfo", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Privilege_MenuInfo", "ParentID", c => c.Int());
            AlterColumn("dbo.Privilege_Privilege", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Privilege_RoleInfo", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_MT_UserInfo_Group", "Group_ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SignalR_Connection", "ID");
            AddPrimaryKey("dbo.Privilege_UserInfo", "ID");
            AddPrimaryKey("dbo.Privilege_MenuInfo", "ID");
            AddPrimaryKey("dbo.Privilege_Privilege", "ID");
            AddPrimaryKey("dbo.Privilege_RoleInfo", "ID");
            AddPrimaryKey("dbo.SignalR_MT_UserInfo_Group", new[] { "UserInfo_ID", "Group_ID" });
            CreateIndex("dbo.SignalR_Connection", "UserInfoID");
            CreateIndex("dbo.Privilege_MenuInfo", "ParentID");
            CreateIndex("dbo.SignalR_MT_UserInfo_Group", "Group_ID");
            AddForeignKey("dbo.SignalR_Connection", "UserInfoID", "dbo.Privilege_UserInfo", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_ID", "dbo.Privilege_UserInfo", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Privilege_MenuInfo", "ParentID", "dbo.Privilege_MenuInfo", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Privilege_MenuInfo", "ParentID", "dbo.Privilege_MenuInfo");
            DropForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_ID", "dbo.Privilege_UserInfo");
            DropForeignKey("dbo.SignalR_Connection", "UserInfoID", "dbo.Privilege_UserInfo");
            DropIndex("dbo.SignalR_MT_UserInfo_Group", new[] { "Group_ID" });
            DropIndex("dbo.Privilege_MenuInfo", new[] { "ParentID" });
            DropIndex("dbo.SignalR_Connection", new[] { "UserInfoID" });
            DropPrimaryKey("dbo.SignalR_MT_UserInfo_Group");
            DropPrimaryKey("dbo.Privilege_RoleInfo");
            DropPrimaryKey("dbo.Privilege_Privilege");
            DropPrimaryKey("dbo.Privilege_MenuInfo");
            DropPrimaryKey("dbo.Privilege_UserInfo");
            DropPrimaryKey("dbo.SignalR_Connection");
            AlterColumn("dbo.SignalR_MT_UserInfo_Group", "Group_ID", c => c.Long(nullable: false));
            AlterColumn("dbo.Privilege_RoleInfo", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Privilege_Privilege", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Privilege_MenuInfo", "ParentID", c => c.Long());
            AlterColumn("dbo.Privilege_MenuInfo", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Privilege_UserInfo", "ID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.SignalR_Connection", "UserInfoID", c => c.Long(nullable: false));
            AlterColumn("dbo.SignalR_Connection", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.SignalR_MT_UserInfo_Group", new[] { "UserInfo_ID", "Group_ID" });
            AddPrimaryKey("dbo.Privilege_RoleInfo", "ID");
            AddPrimaryKey("dbo.Privilege_Privilege", "ID");
            AddPrimaryKey("dbo.Privilege_MenuInfo", "ID");
            AddPrimaryKey("dbo.Privilege_UserInfo", "ID");
            AddPrimaryKey("dbo.SignalR_Connection", "ID");
            CreateIndex("dbo.SignalR_MT_UserInfo_Group", "Group_ID");
            CreateIndex("dbo.Privilege_MenuInfo", "ParentID");
            CreateIndex("dbo.SignalR_Connection", "UserInfoID");
            AddForeignKey("dbo.Privilege_MenuInfo", "ParentID", "dbo.Privilege_MenuInfo", "ID");
            AddForeignKey("dbo.SignalR_MT_UserInfo_Group", "Group_ID", "dbo.Privilege_UserInfo", "ID", cascadeDelete: true);
            AddForeignKey("dbo.SignalR_Connection", "UserInfoID", "dbo.Privilege_UserInfo", "ID", cascadeDelete: true);
        }
    }
}
