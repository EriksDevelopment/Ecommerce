namespace Ecommerce_Api.Data.Models
{
    public enum UserRole
    {
        User,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string UserNumber { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ShoppingCart> CartItems { get; set; } = new List<ShoppingCart>();
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}