using Ecommerce_Api.Data.Dtos.Product;

namespace Ecommerce_Api.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductAddResponseDto> AddAsync(ProductAddRequestDto dto);
        Task<ProductDeleteResponseDto> DeleteAsync(string productNumber);
    }
}