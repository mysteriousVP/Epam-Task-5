using AutoMapper;
using BLL.DTO;

//using DAL_ADO.Entities;

using DAL_EF.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.App_Configuration_Mapper
{
    public static class MappingConfiguration
    {
        public static MapperConfiguration ConfigureMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Product, ProductDTO>();
                    cfg.CreateMap<Category, CategoryDTO>();
                    cfg.CreateMap<Provider, ProviderDTO>();
                    cfg.CreateMap<ProductDTO, Product>();
                    cfg.CreateMap<CategoryDTO, Category>();
                    cfg.CreateMap<ProviderDTO, Provider>();
                });

            return configuration;
        }
    }
}
