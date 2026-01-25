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

        public async Task<User?> GetEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}