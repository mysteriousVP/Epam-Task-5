using System;
using System.Collections.Generic;
using DAL_EF.Interfaces;
using DAL_EF.Entities;
using DAL_EF.Context;
using System.Linq;

namespace DAL_EF.Repository
{
    class ProviderRepository : IRepository<Provider>
    {
        private readonly CatalogDBContext context;

        public ProviderRepository(CatalogDBContext context)
        {
            this.context = context;
        }

        public bool Create(Provider item)
        {
            try
            {
                context.Providers.Add(item);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Provider Get(int id)
        {
            var provider = context.Providers.Find(id);

            return provider;
        }

        public IEnumerable<Provider> GetAll()
        {
            return context.Providers.ToList();
        }

        public bool Remove(int id)
        {
            Provider provider = this.Get(id);

            if (provider != null)
            {
                return this.Remove(provider);
            }

            return false;
        }

        public bool Remove(Provider item)
        {
            try
            {
                context.Providers.Remove(item);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Provider item)
        {
            try
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
