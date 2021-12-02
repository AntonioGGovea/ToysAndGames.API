using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Data.Models;

namespace ToysAndGames.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }


        //fluent api
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasKey(x => x.Id);
            builder.Entity<Product>().Property(x => x.Id);

            builder.Entity<Product>().Property(x => x.Name).IsRequired(true);
            builder.Entity<Product>().Property(x => x.Name).HasMaxLength(50);

            builder.Entity<Product>().Property(x => x.Description).IsRequired(false);
            builder.Entity<Product>().Property(x => x.Description).HasMaxLength(100);

            builder.Entity<Product>().Property(x => x.AgeRestriction).IsRequired(false);
            builder.Entity<Product>().HasCheckConstraint("CK_Product_AgeRestriction", "AgeRestriction >= 0 AND AgeRestriction <= 100");

            builder.Entity<Product>().Property(x => x.Company).IsRequired(true);
            builder.Entity<Product>().Property(x => x.Company).HasMaxLength(50);

            builder.Entity<Product>().Property(x => x.Price).IsRequired(true);
            builder.Entity<Product>().HasCheckConstraint("CK_Product_Price", "Price >= 1 AND Price <= 1000");

        }
    }
}
