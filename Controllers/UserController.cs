using System.Security.Claims;
using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos.User;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginRequestDto dto)
        {
            try
            {
                var result = await _userService.LoginAsync(dto);

                _logger.LogInformation("Login successfull.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while logging in.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("update")]
        public async Task<ActionResult<UserUpdateResponseDto>> Update(UserUpdateRequestDto dto)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _userService.UpdateAsync(dto, user);

                _logger.LogInformation("Profile successfully updated.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while updating profile.");
                return StatusCode(500, "Something went wrong.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("delete/{password}")]
        public async Task<ActionResult<UserDeleteResponseDto>> Delete(string password)
        {
            try
            {
                var user = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var result = await _userService.DeleteAsync(password, user);

                _logger.LogInformation("User successfully deleted.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while deleting user.");
                return StatusCode(500, "Something went wrong.");
            }

        }
    }
}