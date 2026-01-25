using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface IUserRepo
    {
        Task AddUserAsync(User user);
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
    }
}