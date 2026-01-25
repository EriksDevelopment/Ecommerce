using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Dtos.Wallet;

namespace Ecommerce_Api.Core.Service
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepo _walletRepo;
        private readonly IUserRepo _userRepo;
        public WalletService(IWalletRepo walletRepo, IUserRepo userRepo)
        {
            _walletRepo = walletRepo;
            _userRepo = userRepo;
        }

        public async Task<List<WalletViewResponseDto>> OverViewAccountAsync(int userId)
        {
            var wallet = await _walletRepo.OverViewAccountAsync(userId);
            if (!wallet.Any())
                throw new ArgumentException("No account found.");

            return wallet.Select(w => new WalletViewResponseDto
            {
                Description = w.Description,
                Amount = w.Amount,
                AccountNumber = w.AccountNumber,
                CreatedAt = w.CreatedAt
            }).ToList();
        }
    }
}