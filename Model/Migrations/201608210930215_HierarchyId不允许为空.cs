namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HierarchyId不允许为空 : DbMigration
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
