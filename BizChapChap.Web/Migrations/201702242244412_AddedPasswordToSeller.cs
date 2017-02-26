namespace BizChapChap.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPasswordToSeller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sellers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sellers", "Password");
        }
    }
}
