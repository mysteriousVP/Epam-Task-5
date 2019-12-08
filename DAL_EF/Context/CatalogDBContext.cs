using System.Data.Entity;
using DAL_EF.Entities;
using DAL_EF.Configuration;
using DAL_EF.Configurations;

namespace DAL_EF.Context
{
    [DbConfigurationType(typeof(CatalogContextConfiguration))]
    public class CatalogDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public CatalogDBContext() : base("Catalog")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProviderConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
