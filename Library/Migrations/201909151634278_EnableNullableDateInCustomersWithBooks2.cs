namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableNullableDateInCustomersWithBooks2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomersWithBooks", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomersWithBooks", "ReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
