namespace Ecommerce_Api.Data.Dtos.shoppingCart
{
    public class ViewCartItemsResponseDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int Quantity { get; set; }
        public string ProductNumber { get; set; } = null!;
    }
}