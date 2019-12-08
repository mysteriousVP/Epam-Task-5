 using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBusinessService
    {
        Task<IEnumerable<ProviderDTO>> GetProvidersByEmail(string email);
        Task<IEnumerable<ProductDTO>> GetProductsWithMinPrice();
        Task<IEnumerable<ProductDTO>> GetProductsWithMaxPrice();
        Task<IEnumerable<ProductDTO>> GetProductsByPrice(double price);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<IEnumerable<ProductDTO>> GetProductByProvider(int providerId);
        Task<IEnumerable<ProviderDTO>> GetProviderByCategory(int categoryId);
        Task<CategoryDTO> GetCategoryWithMinAmountOfProducts();
        Task<CategoryDTO> GetCategoryWithMaxAmountOfProducts();
    }
}
