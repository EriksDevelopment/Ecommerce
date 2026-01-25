using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface IWalletRepo
    {
        Task AddAsync(Wallet wallet);
    }
}