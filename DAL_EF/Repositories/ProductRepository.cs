using System;
using System.Collections.Generic;
using DAL_EF.Interfaces;
using DAL_EF.Entities;
using DAL_EF.Context;
using System.Linq;

namespace DAL_EF.Repository
{
    class ProductRepository : IRepository<Product>
    {
        private readonly CatalogDBContext context;

        public ProductRepository(CatalogDBContext context)
        {
            this.context = context;
        }

        public bool Create(Product item)
        {
            try
            {
                context.Products.Add(item);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Product Get(int id)
        {
            var product = context.Products.Find(id);

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public bool Remove(int id)
        {
            Product product = this.Get(id);

            if (product != null)
            {
                return this.Remove(product);
            }

            return false;
        }

        public bool Remove(Product item)
        {
            try
            {
                context.Products.Remove(item);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Product item)
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
