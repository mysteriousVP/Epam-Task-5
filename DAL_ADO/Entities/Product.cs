using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ADO.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime DateOfCreating { get; set; }
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
    }
}
