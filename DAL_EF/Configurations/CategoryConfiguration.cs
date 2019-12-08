using System.Data.Entity.ModelConfiguration;
using DAL_EF.Entities;

namespace DAL_EF.Configuration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasMany(x => x.Products)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
