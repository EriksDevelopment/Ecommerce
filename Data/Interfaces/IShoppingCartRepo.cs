using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface IShoppingCartRepo
    {
        Task AddAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart?> itemExists(int userId, int productId);
        Task UpdateAsync(ShoppingCart shoppingCart);
    }
}