using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos.shoppingCart;
using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepo _shoppingCartRepo;
        private readonly IProductRepo _productRepo;

        public ShoppingCartService(IShoppingCartRepo shoppingCartRepo, IProductRepo productRepo)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _productRepo = productRepo;
        }

        public async Task<AddToShoppingCartResponseDto> AddProductToCartAsync(AddToShoppingCartRequestDto dto, int userId)
        {
            if (dto.Quantity <= 0)
                throw new ArgumentException("Quantity must be at least 1");

            var product = await _productRepo.GetProductNumberAsync(dto.ProductNumber);

            if (product == null)
                throw new Exception("Product not found");

            var existingItem = await _shoppingCartRepo.ItemExists(userId, product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                await _shoppingCartRepo.UpdateAsync(existingItem);
            }
            else
            {
                var shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = product.Id,
                    Quantity = dto.Quantity,
                    Product = product
                };
                await _shoppingCartRepo.AddAsync(shoppingCart);
                existingItem = shoppingCart;
            }

            return new AddToShoppingCartResponseDto
            {
                Message = "Product added to cart.",
                Name = existingItem.Product.Name,
                Description = existingItem.Product.Description,
                Price = existingItem.Product.Price,
                Category = existingItem.Product.Category.Name,
                ProductNumber = existingItem.Product.ProductNumber
            };
        }

        public async Task<List<ViewCartItemsResponseDto>> GetCartItemsAsync(int userId)
        {
            var cartItems = await _shoppingCartRepo.GetCartItemsAsync(userId);
            if (!cartItems.Any())
                throw new ArgumentException("No products in cart.");

            return cartItems.Select(c => new ViewCartItemsResponseDto
            {
                Name = c.Product.Name,
                Description = c.Product.Description,
                Price = c.Product.Price,
                Category = c.Product.Category.Name,
                Quantity = c.Quantity,
                ProductNumber = c.Product.ProductNumber
            }).ToList();
        }
    }
}
