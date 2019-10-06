namespace Library.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoverToBookTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookCover", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BookCover");
        }
    }
}
