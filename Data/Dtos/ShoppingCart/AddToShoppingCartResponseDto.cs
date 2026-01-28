namespace Ecommerce_Api.Data.Dtos.shoppingCart
{
    public class AddToShoppingCartResponseDto
    {
        public string Message { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string ProductNumber { get; set; } = null!;
    }
}