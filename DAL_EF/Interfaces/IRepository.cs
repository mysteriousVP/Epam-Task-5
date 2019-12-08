using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        void Remove(int id);
        void Remove(T item);
        void Update(T item);
    }
}
