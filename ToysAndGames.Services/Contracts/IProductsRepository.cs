using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Data.Models;

namespace ToysAndGames.Services.Contracts
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetProducts(
            Expression<Func<Product, bool>> expression = null,
            Func<IEnumerable<Product>, IOrderedQueryable<Product>> orderBy = null);

        Product GetProduct(Expression<Func<Product, bool>> expression);

        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool Save();
    }
}
