using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface IShoppingCartRepo
    {
        Task AddAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart?> ItemExists(int userId, int productId);
        Task UpdateAsync(ShoppingCart shoppingCart);

        Task<List<ShoppingCart>> GetCartItemsAsync(int userId);
    }
}