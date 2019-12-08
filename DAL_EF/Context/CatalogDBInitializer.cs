using System.Collections.Generic;
using DAL_EF.Entities;
using System.Data.Entity;
using System.Linq;

namespace DAL_EF.Context
{
    class CatalogDBInitializer : DropCreateDatabaseIfModelChanges<CatalogDBContext>
    {
        protected override void Seed(CatalogDBContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category() { Name = "Vegetables" },
                new Category() { Name = "Eggs" },
                new Category() { Name = "Groats" },
                new Category() { Name = "Pasta" },
                new Category() { Name = "Meat" },
                new Category() { Name = "Fish" },
                new Category() { Name = "Spice" },
                new Category() { Name = "Vegetable oil" }
            };

            categories.ForEach(x => context.Categories.Add(x));
            context.SaveChanges();

            List<Provider> providers = new List<Provider>()
            {
                new Provider() {Name = "Farm 1"},
                new Provider() {Name = "Farm 2"},
                new Provider() {Name = "Farm 3"},
                new Provider() {Name = "Farm 4"},
                new Provider() {Name = "Farm 5"},
                new Provider() {Name = "Farm 6"},
                new Provider() {Name = "Farm 7"},
                new Provider() {Name = "Farm 8"}
            };

            providers.ForEach(x => context.Providers.Add(x));
            context.SaveChanges();

            List<Product> products = new List<Product>()
            {
                new Product () {Name = "Product 1" ,
                                Price = 10,
                                CategoryId = context.Categories.Where(tmp => tmp.Name == "Meat").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 1").First().ProviderId},
                new Product () {Name = "Product 2",
                                Price = 20,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Pasta").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 1").First().ProviderId},
                new Product () {Name = "Product 3",
                                Price = 30,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Fish").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 4").First().ProviderId},
                new Product () {Name = "Product 4",
                                Price = 40,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Eggs").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 1").First().ProviderId},
                new Product () {Name = "Product 5",
                                Price = 1000,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Vegetable oil").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 2").First().ProviderId},
                new Product () {Name = "Product 6",
                                Price = 60,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Vegetables").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 2").First().ProviderId},
                new Product () {Name = "Product 7",
                                Price = 150,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Spice").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 8").First().ProviderId},
                new Product () {Name = "Product 8",
                                Price = 50,
                                CategoryId = context.Categories.Where(tmp=> tmp.Name == "Groats").First().CategoryId,
                                ProviderId = context.Providers.Where(tmp => tmp.Name == "Farm 5").First().ProviderId}
            };

            products.ForEach(x => context.Products.Add(x));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
