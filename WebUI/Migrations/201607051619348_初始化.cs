using System.Data.Entity.Migrations;

namespace WebUI.Migrations
{
    public partial class 初始化 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Content = c.String(),
                        Description = c.String(),
                        Uinque = c.Guid(nullable: false, identity: true),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Article");
        }
    }
}
