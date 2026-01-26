using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;

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
    }
}