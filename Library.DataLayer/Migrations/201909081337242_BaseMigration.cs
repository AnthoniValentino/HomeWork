namespace Library.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    firstName = c.String(nullable: false, maxLength: 100),
                    lastName = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    authorId = c.Int(nullable: false),
                    title = c.String(nullable: false, maxLength: 500),
                    price = c.Int(),
                    pages = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Authors", t => t.authorId)
                .Index(t => t.authorId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "authorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "authorId" });
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
