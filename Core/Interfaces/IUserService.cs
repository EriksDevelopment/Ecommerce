using Ecommerce_Api.Data.Dtos.User;

namespace Ecommerce_Api.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseDto> AddAsync(UserRegisterRequestDto dto);
        Task<UserLoginResponseDto> LoginAsync(UserLoginRequestDto dto);
    }
}