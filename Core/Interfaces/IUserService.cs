using Ecommerce_Api.Data.Dtos;

namespace Ecommerce_Api.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseDto> AddAsync(UserRegisterRequestDto dto);
    }
}