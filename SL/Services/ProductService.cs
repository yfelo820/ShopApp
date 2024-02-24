using DAL.DB_Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _shopContext;

        public ProductService(ShopContext shopContext) => _shopContext = shopContext;

        public async Task<Product> AddProduct(Product product)
        {
            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();

            return product;
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _shopContext.Products
                                            .FirstOrDefaultAsync(x => x.ProductId == id);

            if(product == null)
            {
                return (int)default;
            }

            _shopContext.Products.Remove(product);
            return await _shopContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> FiltersProductsByName(string productName)
        {
            return await _shopContext.Products
                                     .Where(p => p.Name.Contains(productName))
                                     .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _shopContext.Products
                                     .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _shopContext.Products
                                            .FirstOrDefaultAsync(x => x.ProductId == id);

            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _shopContext.Entry(product).State = EntityState.Modified;
            await _shopContext.SaveChangesAsync();

            return product;
        }
    }

    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductById(int id);

        Task<Product> AddProduct(Product product);

        Task<Product> UpdateProduct(Product product);

        Task<int> DeleteProduct(int id);

        Task<IEnumerable<Product>> FiltersProductsByName(string productName);
    }
}
