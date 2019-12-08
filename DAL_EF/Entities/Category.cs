using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL_EF.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
