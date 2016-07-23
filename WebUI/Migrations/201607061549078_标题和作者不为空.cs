using System.Data.Entity.Migrations;

namespace WebUI.Migrations
{
    public partial class 标题和作者不为空 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Article", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Article", "Author", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Article", "Author", c => c.String());
            AlterColumn("dbo.Article", "Title", c => c.String());
        }
    }
}
