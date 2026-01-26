using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface ICategoryRepo
    {
        Task<bool> CategoryExistsAsync(int id);
        Task<Category?> GetByIdAsync(int id);
    }
}