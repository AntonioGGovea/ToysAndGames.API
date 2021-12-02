using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToysAndGames.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 100)]
        public int? AgeRestriction { get; set; }

        public string Company { get; set; }

        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}
