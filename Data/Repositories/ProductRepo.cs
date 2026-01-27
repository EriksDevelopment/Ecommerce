using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public async Task<List<Product>> SearchProductAsync(string? name, string? category)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, $"%{name}%"));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Category.Name, $"%{category}%"));
            }

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
    }
}