using System;
using System.Collections.Generic;
using DAL_EF.Interfaces;
using DAL_EF.Entities;
using DAL_EF.Context;
using System.Linq;

namespace DAL_EF.Repository
{
    class CategoryRepository : IRepository<Category>
    {
        private readonly CatalogDBContext context;

        public CategoryRepository(CatalogDBContext context)
        {
            this.context = context;
        }

        public bool Create(Category item)
        {
            try
            {
                context.Categories.Add(item);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category Get(int id)
        {
            var category = context.Categories.Find(id);

            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public bool Remove(int id)
        {

            Category category = this.Get(id);

            if (category != null)
            {
                return this.Remove(category);
            }

            return false;
        }

        public bool Remove(Category item)
        {
            try
            {
                context.Categories.Remove(item);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Category item)
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
