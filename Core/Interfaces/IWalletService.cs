using Ecommerce_Api.Data.Dtos.Wallet;

namespace Ecommerce_Api.Core.Interfaces
{
    public interface IWalletService
    {
        Task<List<WalletViewResponseDto>> OverViewAccountAsync(int userId);
    }
}