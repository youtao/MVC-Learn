namespace MVCLearn.ModelDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改表名 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Privilege_UserInfo", newName: "System_UserInfo");
            RenameTable(name: "dbo.Privilege_MenuInfo", newName: "System_MenuInfo");
            RenameTable(name: "dbo.Privilege_RoleInfo", newName: "System_RoleInfo");
            DropTable("dbo.Privilege_Privilege");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Privilege_Privilege",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrivilegeName = c.String(),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            RenameTable(name: "dbo.System_RoleInfo", newName: "Privilege_RoleInfo");
            RenameTable(name: "dbo.System_MenuInfo", newName: "Privilege_MenuInfo");
            RenameTable(name: "dbo.System_UserInfo", newName: "Privilege_UserInfo");
        }
    }
}
