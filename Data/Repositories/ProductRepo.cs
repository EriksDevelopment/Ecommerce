using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Data.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly EcommerceDbContext _context;
        public ProductRepo(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductNumberAsync(string productNumber) =>
            await _context.Products.FirstOrDefaultAsync(p => p.ProductNumber == productNumber);

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> ViewProductAsync() =>
            await _context.Products
            .Include(p => p.Category)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}