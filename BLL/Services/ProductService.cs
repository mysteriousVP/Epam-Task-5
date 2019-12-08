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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork UnitOFWork;
        private readonly IMapper Mapper;

        public ProductService(IUnitOfWork uow)
        {
            this.UnitOFWork = uow ?? throw new NullReferenceException();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public ProductService()
        {
            this.UnitOFWork = new UnitOfWork();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public bool Add(ProductDTO item)
        {
            Product newProduct = Mapper.Map<ProductDTO, Product>(item);
            UnitOFWork.Products.Create(newProduct);

            if (UnitOFWork.SaveChanges())
            {
                return true;
            }

            return false;
        }

        public async Task<ProductDTO> Get(int id)
        {
            Product product = await UnitOFWork.Products.Get(id);

            return Mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(await UnitOFWork.Products.GetAll());
        }

        public bool Remove(ProductDTO item)
        {
            Product product = Mapper.Map<ProductDTO, Product>(item);
            UnitOFWork.Products.Remove(product);

            if (!UnitOFWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            Product product = await UnitOFWork.Products.Get(id);

            if (product == null)
            {
                return false;
            }

            UnitOFWork.Products.Remove(product);

            if (!UnitOFWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public bool Update(ProductDTO item)
        {
            Product product = Mapper.Map<ProductDTO, Product>(item);
            UnitOFWork.Products.Update(product);

            if (!UnitOFWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            IEnumerable<Product> allProducts = await UnitOFWork.Products.GetAll();

            var selectedProducts = allProducts.Where(p => p.CategoryId == categoryId);

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByProvider(int providerId)
        {
            IEnumerable<Product> allProducts = await UnitOFWork.Products.GetAll();

            var selectedProducts = allProducts.Where(p => p.ProviderId == providerId);

            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(selectedProducts);
        }
    }
}
