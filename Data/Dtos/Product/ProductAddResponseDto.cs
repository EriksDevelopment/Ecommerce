namespace Ecommerce_Api.Data.Dtos.Product
{
    public class ProductAddResponseDto
    {
        public string Message { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; } = null!;
        public string ProductNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}