namespace BizChapChap.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnTitleToListing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "Title");
        }
    }
}
