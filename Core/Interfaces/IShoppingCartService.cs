using Ecommerce_Api.Data.Dtos.shoppingCart;

namespace Ecommerce_Api.Core.Interfaces
{
    public interface IShoppingCartService
    {
        Task<AddToShoppingCartResponseDto> AddProductToCartAsync(AddToShoppingCartRequestDto dto, int userId);
    }
}