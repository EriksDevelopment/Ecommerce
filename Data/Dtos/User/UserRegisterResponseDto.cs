namespace Ecommerce_Api.Data.Dtos.User
{
    public class UserRegisterResponseDto
    {
        public string Message { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string UserNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}