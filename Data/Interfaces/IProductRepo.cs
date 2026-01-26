using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Data.Interfaces
{
    public interface IProductRepo
    {
        Task AddAsync(Product product);
    }
}