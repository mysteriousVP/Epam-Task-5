using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ADO.Interfaces
{
    public interface IGateway<T> where T : class
    {
        bool Create(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        bool Remove(int id);
        bool Remove(T item);
        bool Update(T item);
    }
}
