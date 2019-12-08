using DAL_EF.Context;
using System.Data.Entity;

namespace DAL_EF.Configurations
{
    public class CatalogContextConfiguration : DbConfiguration
    {
        public CatalogContextConfiguration()
        {
            this.SetDatabaseInitializer<CatalogDBContext>(new CatalogDBInitializer());
        }
    }
}
