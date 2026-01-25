using System.Security.Claims;
using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletController> _logger;
        public WalletController(IWalletService walletService, ILogger<WalletController> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        [HttpGet("Overview")]
        public async Task<ActionResult<List<WalletViewResponseDto>>> OverView()
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _walletService.OverViewAccountAsync(user);

                _logger.LogInformation("All wallets retrieved.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while retrieving user wallets");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}