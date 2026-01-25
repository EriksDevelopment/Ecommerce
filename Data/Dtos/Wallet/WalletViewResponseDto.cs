namespace Ecommerce_Api.Data.Dtos.Wallet
{
    public class WalletViewResponseDto
    {
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}