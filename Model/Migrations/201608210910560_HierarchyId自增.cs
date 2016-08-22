namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HierarchyId自增 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.System_Department", "Path", c => c.HierarchyId(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.System_Department", "Path", c => c.HierarchyId());
        }
    }
}
