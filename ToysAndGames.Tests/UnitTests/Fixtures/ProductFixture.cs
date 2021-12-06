using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Data.Models;
using ToysAndGames.DTOs;

namespace ToysAndGames.Tests.UnitTests.Fixtures
{
    public class ProductFixture
    {
        // List of products
        public List<Product> products => new List<Product>() { product1, product2, product3};

        // Individual products
        public Product product1 => new Product()
        {
            Id = 1,
            Name = "product1",
            Description = "product one",
            AgeRestriction = 12,
            Company = "company1",
            Price = 20
        };

        public Product product2 => new Product()
        {
            Id = 1,
            Name = "product1",
            Description = null,
            AgeRestriction = 12,
            Company = "company1",
            Price = 20
        };

        public Product product3 => new Product()
        {
            Id = 1,
            Name = "product1",
            Description = "product one",
            AgeRestriction = null,
            Company = "company1",
            Price = 20
        };

        // product DTOs
        public CreateProductDTO CreateProductDto => new CreateProductDTO()
        {
            Name = product1.Name,
            Description = product1.Description,
            AgeRestriction = product1.AgeRestriction,
            Company = product1.Company,
            Price = product1.Price
        };

        public ProductDTO ProductDTO => new ProductDTO()
        {
            Id = product1.Id,
            Name = product1.Name,
            Description = product1.Description,
            AgeRestriction = product1.AgeRestriction,
            Company = product1.Company,
            Price = product1.Price
        };
    }
}
