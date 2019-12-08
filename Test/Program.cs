using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Services;
using DAL_EF;
//using DAL_ADO;
using DAL_EF.Context;
using DAL_EF.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Test
{
    class Program
    {
        static List<ProductDTO> GetAll(ProductService productService)
        {
            var result = productService.GetAll().Result.ToList();
            return result;
        }
        static void Main(string[] args)
        {
            //BusinessService businessService = new BusinessService();
            //foreach (var item in businessService.GetAllProducts())
            //{
            //    Console.WriteLine(item.ProductId + "    " + item.Name + "    "
            //        + item.Price);
            //}
            //Console.WriteLine();
            ProductService productService = new ProductService();
            bool resultAdd = productService.Add(new ProductDTO()
            {
                Name = "Vehicle", ProviderId = 1, CategoryId = 1, DateOfCreating = DateTime.Now
            });
            var listone = GetAll(productService);
            foreach (var item in listone)
            {
                Console.WriteLine(item.Name);
            }
            bool result = productService.Remove(productService.GetAll().Result.Where(p => p.Name == "213").FirstOrDefault().ProductId).Result;
            var list = GetAll(productService);
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
            Console.ReadLine();
        }
    }
}
