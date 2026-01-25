namespace Ecommerce_Api.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public enum OrderStatus
        {
            Pending,
            Completed,
            Cancelled
        }

        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}