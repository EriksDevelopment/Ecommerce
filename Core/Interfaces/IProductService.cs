using Ecommerce_Api.Data.Dtos.Product;

namespace Ecommerce_Api.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductAddResponseDto> AddAsync(ProductAddRequestDto dto);
        Task<ProductDeleteResponseDto> DeleteAsync(string productNumber);
        Task<List<ProductViewResponseDto>> GetAsync(string? name, string? category);
        Task<ProductUpdateResponseDto> UpdateAsync(ProductUpdateRequestDto dto, int id);
    }
}