namespace BizChapChap.Web.Migrations
{
    using BizChapChap.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BizChapChap.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BizChapChap.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Region.AddOrUpdate(
              p => p.Name,
              new Region { Name = "Dar es Salaam" },
              new Region { Name = "Mwanza" },
              new Region { Name = "Arusha" },
              new Region { Name = "Mbeya" }
            );

            context.Category.AddOrUpdate(
                p => p.Name,
                new Category { Name ="Real Estate"},
                new Category { Name = "Electronics"},
                new Category { Name = "Vehicles"},
                new Category { Name = "Personal Items" },
                new Category { Name = "Home Items" },
                new Category { Name = "Travel" },
                new Category { Name = "Jobs & Services" },
                new Category { Name = "Others" }
                );

            context.SaveChanges();

            //get categories => subCategory foreign keys
            var categories = context
                            .Category
                            .ToDictionary(p => p.Name, p => p.Id);

            context.SubCategory.AddOrUpdate(
                new SubCategory() { Name = "Houses For Sell", CategoryRefId = categories["Real Estate"]},
                new SubCategory() { Name = "Houses For Rent", CategoryRefId = categories["Real Estate"] },
                new SubCategory() { Name = "Land", CategoryRefId = categories["Real Estate"] },
                new SubCategory() { Name = "Audio & Video", CategoryRefId = categories["Electronics"] },
                new SubCategory() { Name = "Computer Software", CategoryRefId = categories["Electronics"] },
                new SubCategory() { Name = "Computer Hardware", CategoryRefId = categories["Electronics"] },
                new SubCategory() { Name = "Engineering Jobs", CategoryRefId = categories["Jobs & Services"] }
                );
            context.SaveChanges();
        }
    }
}
