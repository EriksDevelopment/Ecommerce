using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos.Product;
using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Core.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        public ProductService(IProductRepo productRepo, ICategoryRepo categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<ProductAddResponseDto> AddAsync(ProductAddRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Name or description can't be empty.");

            if (dto.Price <= 0)
                throw new ArgumentException("Price can't be 0 or lower.");

            if (dto.StockQuantity < 0)
                throw new ArgumentException("Stock quantity can't be less than 0.");

            if (!await _categoryRepo.CategoryExistsAsync(dto.CategoryId))
                throw new ArgumentException("Category does not exist.");

            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new ArgumentException("Category not found.");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = category.Id,
                Category = category
            };

            await _productRepo.AddAsync(product);

            return new ProductAddResponseDto
            {
                Message = "Product added.",
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = category.Name,
                ProductNumber = product.ProductNumber,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductDeleteResponseDto> DeleteAsync(string productNumber)
        {
            if (string.IsNullOrWhiteSpace(productNumber))
                throw new ArgumentException("Product number can't be empty.");

            var product = await _productRepo.GetProductNumberAsync(productNumber);
            if (product == null)
                throw new ArgumentException("Product number not found.");

            await _productRepo.DeleteAsync(product);

            return new ProductDeleteResponseDto
            {
                Message = $"Product with id {product.ProductNumber} deleted.",
                ProductName = product.Name
            };
        }
    }
}