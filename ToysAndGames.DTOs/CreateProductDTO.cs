using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToysAndGames.DTOs
{
    public class CreateProductDTO
    {
        [MaxLength(50), Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Range(0, 100)]
        public int? AgeRestriction { get; set; }
        [MaxLength(50), Required]
        public string Company { get; set; }
        [Range(1, 1000), Required]
        public decimal Price { get; set; }
    }
}
