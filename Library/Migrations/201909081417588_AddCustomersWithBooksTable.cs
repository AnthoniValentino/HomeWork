namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomersWithBooksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomersWithBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Period = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomersWithBooks", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomersWithBooks", "BookId", "dbo.Books");
            DropIndex("dbo.CustomersWithBooks", new[] { "BookId" });
            DropIndex("dbo.CustomersWithBooks", new[] { "CustomerId" });
            DropTable("dbo.CustomersWithBooks");
        }
    }
}
