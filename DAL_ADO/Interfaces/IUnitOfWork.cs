using DAL_ADO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ADO.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGateway<Category> Categories { get; }
        IGateway<Product> Products { get; }
        IGateway<Provider> Providers { get; }
    }
}
