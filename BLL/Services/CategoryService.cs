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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public CategoryService(IUnitOfWork uow)
        {
            this.UnitOfWork = uow ?? throw new NullReferenceException();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public CategoryService()
        {
            this.UnitOfWork = new UnitOfWork();
            this.Mapper = MappingConfiguration.ConfigureMapper().CreateMapper();
        }

        public bool Add(CategoryDTO item)
        {
            Category newCategory = Mapper.Map<Category>(item);
            UnitOfWork.Categories.Create(newCategory);
               
            if (UnitOfWork.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            Category category = await UnitOfWork.Categories.Get(id);

            return Mapper.Map<Category, CategoryDTO>(category);
        }
            
        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(await UnitOfWork.Categories.GetAll());
        }

        public bool Remove(CategoryDTO item)
        {
            UnitOfWork.Categories.Remove(Mapper.Map<CategoryDTO, Category>(item));

            return UnitOfWork.SaveChanges();
        }

        public async Task<bool> Remove(int id)
        {
            Category category = await UnitOfWork.Categories.Get(id);

            if (category == null)
            {
                return false;
            }

            UnitOfWork.Categories.Remove(category);

            if (!UnitOfWork.SaveChanges())
            {
                return false;
            }

            return true;
        }

        public bool Update(CategoryDTO item)
        {
            Category category = Mapper.Map<CategoryDTO, Category>(item);

            UnitOfWork.Categories.Update(category);

            if (!(UnitOfWork.SaveChanges()))
            {
                return false;
            }

            return true;
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
    }
}
