using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult<ProductAddResponseDto>> Add(ProductAddRequestDto dto)
        {
            try
            {
                var result = await _productService.AddAsync(dto);

                _logger.LogInformation("Product successfully added.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while adding product.");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}