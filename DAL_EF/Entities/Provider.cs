using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL_EF.Entities
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        public string Email { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}