using DAL_EF.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAL_EF.Configuration
{
    public class ProviderConfiguration : EntityTypeConfiguration<Provider>
    {
        public ProviderConfiguration()
        {
            HasMany(x => x.Products)
                .WithRequired(x => x.Provider)
                .HasForeignKey(x => x.ProviderId);
        }
    }
}
