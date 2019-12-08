using System;
using System.Threading.Tasks;
using DAL_EF.Entities;

namespace DAL_EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        IRepository<Provider> Providers { get; }
        bool SaveChanges();
    }
}
