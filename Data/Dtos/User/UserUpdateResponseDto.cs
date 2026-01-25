namespace Ecommerce_Api.Data.Dtos.User
{
    public class UserUpdateResponseDto
    {
        public string Message { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}