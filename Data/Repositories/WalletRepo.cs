using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Repositories
{
    public class WalletRepo : IWalletRepo
    {
        private readonly EcommerceDbContext _context;
        public WalletRepo(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }
    }
}