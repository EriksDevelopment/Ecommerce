using System.Security.Claims;
using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos.shoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;
        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpPost("add")]
        public async Task<ActionResult<AddToShoppingCartResponseDto>> Add(
            [FromBody] AddToShoppingCartRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _shoppingCartService.AddProductToCartAsync(dto, user);

                _logger.LogInformation("Product added to cart.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while adding product to cart.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<List<ViewCartItemsResponseDto>>> Get()
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _shoppingCartService.GetCartItemsAsync(user);

                _logger.LogInformation("All products in cart retrieved.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving all products in cart.");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}