using DAL_EF.Context;
using DAL_EF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL_EF.Entities;

namespace DAL_EF
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CatalogDBContext context;
        private readonly DbSet<T> dBSet;

        public Repository(CatalogDBContext context)
        {
            this.context = context;
            this.dBSet = context.Set<T>();
        }

        public void Create(T item)
        {
            dBSet.Add(item);
        }

        public async Task<T> Get(int id)
        {
            T TEntity = await dBSet.FindAsync(id);

            return TEntity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dBSet.ToListAsync();
        }

        public void Remove(int id)
        {

            T TEntity = dBSet.Find(id);

            if (TEntity != null)
            {
                dBSet.Remove(TEntity);
            }
        }

        public void Remove(T item)
        {
            dBSet.Remove(item);
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;     
        }
    }
}
