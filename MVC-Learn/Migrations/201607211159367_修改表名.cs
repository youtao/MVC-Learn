namespace MVC_Learn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改表名 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserInfo", newName: "System_UserInfo");
            RenameTable(name: "dbo.Menu", newName: "System_Menu");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.System_Menu", newName: "Menu");
            RenameTable(name: "dbo.System_UserInfo", newName: "UserInfo");
        }
    }
}
