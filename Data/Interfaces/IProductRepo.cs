using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface IProductRepo
    {
        Task AddAsync(Product product);
        Task<Product?> GetProductNumberAsync(string productNumber);
        Task DeleteAsync(Product product);
        Task<List<Product>> SearchProductAsync(string? name, string? category);
        Task UpdateAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
    }
}