using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Data.Models;

namespace ToysAndGames.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Name).HasMaxLength(50);

            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(100);

            builder.Property(x => x.AgeRestriction).IsRequired(false);
            builder.HasCheckConstraint("CK_Product_AgeRestriction", "AgeRestriction >= 0 AND AgeRestriction <= 100");

            builder.Property(x => x.Company).IsRequired(true);
            builder.Property(x => x.Company).HasMaxLength(50);

            builder.Property(x => x.Price).IsRequired(true);
            builder.HasCheckConstraint("CK_Product_Price", "Price >= 1 AND Price <= 1000");


            // seed data
            builder.HasData(new Product()
            {
                Id = 1,
                Name = "product1",
                Description = "product one",
                AgeRestriction = 12,
                Company = "company1",
                Price = 20
            });

            builder.HasData(new Product()
            {
                Id = 2,
                Name = "product2",
                Description = null,
                AgeRestriction = 6,
                Company = "company2",
                Price = 100
            });

            builder.HasData(new Product()
            {
                Id = 3,
                Name = "product3",
                Description = "product three",
                AgeRestriction = null,
                Company = "company3",
                Price = 75
            });
        }
    }
}
