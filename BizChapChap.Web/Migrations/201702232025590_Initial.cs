namespace BizChapChap.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryRefId, cascadeDelete: true)
                .Index(t => t.CategoryRefId);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Symbol = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DateEdit = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        CurrencyRefId = c.Int(nullable: false),
                        Description = c.String(),
                        SubCategoryRefId = c.Int(nullable: false),
                        IsPremium = c.Boolean(nullable: false),
                        Photo1 = c.String(),
                        Photo2 = c.String(),
                        Photo3 = c.String(),
                        Photo4 = c.String(),
                        Photo5 = c.String(),
                        Photo6 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryRefId, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyRefId, cascadeDelete: true)
                .Index(t => t.CurrencyRefId)
                .Index(t => t.SubCategoryRefId);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Username = c.String(nullable: false),
                        Phone = c.String(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SellerTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.SellerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sellers", "Type_Id", "dbo.SellerTypes");
            DropForeignKey("dbo.Listings", "CurrencyRefId", "dbo.Currencies");
            DropForeignKey("dbo.Listings", "SubCategoryRefId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryRefId", "dbo.Categories");
            DropIndex("dbo.Sellers", new[] { "Type_Id" });
            DropIndex("dbo.Listings", new[] { "SubCategoryRefId" });
            DropIndex("dbo.Listings", new[] { "CurrencyRefId" });
            DropIndex("dbo.SubCategories", new[] { "CategoryRefId" });
            DropTable("dbo.SellerTypes");
            DropTable("dbo.Sellers");
            DropTable("dbo.Listings");
            DropTable("dbo.Currencies");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
        }
    }
}
