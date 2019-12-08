using DAL_ADO.Entities;
using DAL_ADO.Gateways;
using DAL_ADO.Interfaces;
using System;

namespace DAL_ADO
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGateway<Category> categories;
        private readonly IGateway<Product> products;
        private readonly IGateway<Provider> providers;
        public IGateway<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    return new CategoryGateway();
                }

                return categories;
            }
        }

        public IGateway<Product> Products
        {
            get
            {
                if (products == null)
                {
                    return new ProductGateway();
                }

                return products;
            }
        }

        public IGateway<Provider> Providers
        {
            get
            {
                if (providers == null)
                {
                    return new ProviderGateway();
                }

                return providers;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
