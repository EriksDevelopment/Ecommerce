namespace Ecommerce_Api.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public string ProductNumber { get; set; } = Guid.NewGuid().ToString();

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ShoppingCart> CartItems { get; set; } = new List<ShoppingCart>();
    }
}