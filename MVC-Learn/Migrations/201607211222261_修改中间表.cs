namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改中间表 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupUserInfoes", newName: "SignalR_MT_UserInfo_Group");
            RenameColumn(table: "dbo.SignalR_MT_UserInfo_Group", name: "Group_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.SignalR_MT_UserInfo_Group", name: "UserInfo_Id", newName: "Group_Id");
            RenameColumn(table: "dbo.SignalR_MT_UserInfo_Group", name: "__mig_tmp__0", newName: "UserInfo_Id");
            RenameIndex(table: "dbo.SignalR_MT_UserInfo_Group", name: "IX_Group_Id", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.SignalR_MT_UserInfo_Group", name: "IX_UserInfo_Id", newName: "IX_Group_Id");
            RenameIndex(table: "dbo.SignalR_MT_UserInfo_Group", name: "__mig_tmp__0", newName: "IX_UserInfo_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SignalR_MT_UserInfo_Group", name: "IX_UserInfo_Id", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.SignalR_MT_UserInfo_Group", name: "IX_Group_Id", newName: "IX_UserInfo_Id");
            RenameIndex(table: "dbo.SignalR_MT_UserInfo_Group", name: "__mig_tmp__0", newName: "IX_Group_Id");
            RenameColumn(table: "dbo.SignalR_MT_UserInfo_Group", name: "UserInfo_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.SignalR_MT_UserInfo_Group", name: "Group_Id", newName: "UserInfo_Id");
            RenameColumn(table: "dbo.SignalR_MT_UserInfo_Group", name: "__mig_tmp__0", newName: "Group_Id");
            RenameTable(name: "dbo.SignalR_MT_UserInfo_Group", newName: "GroupUserInfoes");
        }
    }
}
