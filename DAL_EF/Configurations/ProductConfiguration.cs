using DAL_EF.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAL_EF.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
        }
    }
}

