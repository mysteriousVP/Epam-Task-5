using System;
using System.ComponentModel.DataAnnotations;

namespace DAL_EF.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime DateOfCreating { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}