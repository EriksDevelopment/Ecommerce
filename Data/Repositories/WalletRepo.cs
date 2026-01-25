using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Wallet>> OverViewAccountAsync(int userId) =>
            await _context.Wallets.Where(w => w.UserId == userId).ToListAsync();
    }
}