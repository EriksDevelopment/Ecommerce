using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponseDto>> Add(UserRegisterRequestDto dto)
        {
            try
            {
                var result = await _userService.AddAsync(dto);

                _logger.LogInformation("User register successfull.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while registering user.");
                return StatusCode(500, "Something went wrong.");
            }
        }
    }
}