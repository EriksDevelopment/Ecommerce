using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Data.Repositories
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly EcommerceDbContext _context;
        public ShoppingCartRepo(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCart?> ItemExists(int userId, int productId) =>
            await _context.ShoppingCarts
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        public async Task UpdateAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            await _context.SaveChangesAsync();
        }
    }
}