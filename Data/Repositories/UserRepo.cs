using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Api.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly EcommerceDbContext _context;
        public UserRepo(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserNameExistsAsync(string userName) =>
            await _context.Users.AnyAsync(u => u.UserName == userName);

        public async Task<bool> EmailExistsAsync(string email) =>
            await _context.Users.AnyAsync(u => u.Email == email);

    }
}