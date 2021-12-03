using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Data;
using ToysAndGames.Data.Models;
using ToysAndGames.Services.Contracts;

namespace ToysAndGames.Services.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Product> _db;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<Product>();
        }


        public Product GetProduct(Expression<Func<Product, bool>> expression)
        {
            try
            {
                return _db.FirstOrDefault(expression);
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong with the database");
            }
        }

        public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> expression = null, Func<IEnumerable<Product>, IOrderedQueryable<Product>> orderBy = null)
        {
            try
            {
                IQueryable<Product> query = _db;
                if (expression != null)
                {
                    query = query.Where(expression);
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                return query.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong with the database");
            }
        }

        public bool CreateProduct(Product product)
        {
            _db.Add(product);
            return Save();
        }

        public bool UpdateProduct(Product product)
        {
            _db.Update(product);
            return Save();
        }

        public bool DeleteProduct(Product product)
        {
            _db.Remove(product);
            return Save();
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
