using System;
using DAL_EF.Entities;
using DAL_EF.Interfaces;
using DAL_EF.Context;
using System.Threading.Tasks;

namespace DAL_EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDBContext context;

        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Provider> providerRepository;

        public UnitOfWork()
        {
            context = new CatalogDBContext();
        }

        public IRepository<Category> Categories
        {
            get
            {
                return categoryRepository ?? new Repository<Category>(context);
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                return productRepository ?? new Repository<Product>(context);
            }
        }

        public IRepository<Provider> Providers
        {
            get
            {
                return providerRepository ?? new Repository<Provider>(context);
            }
        }

        private bool disposedState = false;

        protected virtual void Dispose(bool disposingState)
        {
            if (!disposedState)
            {
                if (disposingState)
                {
                    context.Dispose();
                }

                disposedState = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
