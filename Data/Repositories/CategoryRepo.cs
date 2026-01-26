using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Data.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly EcommerceDbContext _context;
        public CategoryRepo(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CategoryExistsAsync(int id) =>
            await _context.Categories.AnyAsync(c => c.Id == id);

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}