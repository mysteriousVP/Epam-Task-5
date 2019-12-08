using AutoMapper;
using BLL.App_Configuration_Mapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL_EF;   
using DAL_EF.Entities;
using DAL_EF.Interfaces; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public BusinessService(IUnitOfWork uow)
        {
            UnitOfWork = uow ?? throw new NullReferenceException();
            Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public BusinessService()
        {
            this.UnitOfWork = new UnitOfWork();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var product = await UnitOfWork.Products.GetAll();
            var selectedProducts = product.Where(prd => prd.CategoryId == categoryId);

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductByProvider(int providerId)
        {
            var product = await UnitOfWork.Products.GetAll();
            var selectedProducts = product.Where(prd => prd.ProviderId == providerId);

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);
        }

        public async Task<IEnumerable<ProviderDTO>> GetProviderByCategory(int categoryId)
        {
            var product = await UnitOfWork.Products.GetAll();
            var selectedProviderId = product.Where(prd => prd.CategoryId == categoryId).Select(x => x.ProviderId).Distinct();

            List<Provider> providers = new List<Provider>();
            foreach (var item in selectedProviderId)
            {
                providers.Add(await UnitOfWork.Providers.Get(item));
            }

            return Mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderDTO>>(providers);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await UnitOfWork.Products.GetAll());
        }

        public async Task<IEnumerable<ProviderDTO>> GetAllProviders()
        {
            return Mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderDTO>>(await UnitOfWork.Providers.GetAll());
        }

        public async Task<CategoryDTO> GetCategoryWithMinAmountOfProducts()
        {
            IEnumerable<Category> allCategories = await UnitOfWork.Categories.GetAll();
            IEnumerable<Product> allProducts = await UnitOfWork.Products.GetAll();

            var selectedCategories = allCategories.GroupJoin(
                inner: allProducts,
                outerKeySelector: categories => categories.CategoryId,
                innerKeySelector: products => products.CategoryId,
                resultSelector: (categories, products) =>
                    new { CatName = categories.Name, Count = products.Count() }
                ).OrderBy(p => p.Count).First();

            return Mapper.Map<Category, CategoryDTO>(allCategories
                .Where(p => p.Name == selectedCategories.CatName).First());
        }

        public async Task<CategoryDTO> GetCategoryWithMaxAmountOfProducts()
        {
            IEnumerable<Category> allCategories = await UnitOfWork.Categories.GetAll();
            IEnumerable<Product> allProducts = await UnitOfWork.Products.GetAll();

            var selectedCategories = allCategories.GroupJoin(
                inner: allProducts,
                outerKeySelector: categories => categories.CategoryId,
                innerKeySelector: products => products.CategoryId,
                resultSelector: (categories, products) =>
                    new { CatName = categories.Name, Count = products.Count() }
                ).OrderByDescending(p => p.Count).First();

            return Mapper.Map<Category, CategoryDTO>(allCategories
                .Where(p => p.Name == selectedCategories.CatName).First());
        }

        public async Task<IEnumerable<ProviderDTO>> GetProvidersWithBiggestAmountOfDifferentProductByCategory()
        {
            IEnumerable<Product> allProducts = await UnitOfWork.Products.GetAll();
            IEnumerable<Provider> allProviders = await UnitOfWork.Providers.GetAll();

            var selectedProviders = allProviders.GroupJoin(
                inner: allProducts,
                outerKeySelector: providers => providers.ProviderId,
                innerKeySelector: products => products.ProviderId,
                resultSelector: (providers, products) =>
                    new { providers.ProviderId, CategoryCount = products.Count() }
                ).OrderByDescending(p => p.CategoryCount);

            List<Provider> providersList = new List<Provider>();
            foreach (var item in selectedProviders)
            {
                if (allProviders.Where(p => p.ProviderId == item.ProviderId) != null)
                {
                    providersList.Add(allProviders.FirstOrDefault(p => p.ProviderId == item.ProviderId));
                }
            }

            return Mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderDTO>>(providersList);
        }

        public async Task<IEnumerable<ProviderDTO>> GetProvidersByEmail(string email)
        {
            IEnumerable<Provider> allProviders = await UnitOfWork.Providers.GetAll();
            IEnumerable<Provider> selectedProviders = allProviders.Where(p => p.Email == email);

            return Mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderDTO>>(selectedProviders);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsWithMinPrice()
        {
            IEnumerable<Product> allProducts = await UnitOfWork.Products.GetAll();
            IEnumerable<Product> selectedProducts = allProducts.Where(p => p.Price == allProducts.Min(e => e.Price));

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsWithMaxPrice()
        {
            IEnumerable<Product> allProducts = await UnitOfWork.Products.GetAll();
            IEnumerable<Product> selectedProducts = allProducts.Where(p => p.Price == allProducts.Max(e => e.Price));

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);

        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByPrice(double price)
        {
            IEnumerable<Product> allProducts = await UnitOfWork.Products.GetAll();
            IEnumerable<Product> selectedProducts = allProducts.Where(p => p.Price == price);

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);
        }
    }
}
