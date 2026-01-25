using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Data.Dtos;
using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserRegisterResponseDto> AddAsync(UserRegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.PasswordHash) ||
                string.IsNullOrWhiteSpace(dto.Address) ||
                string.IsNullOrWhiteSpace(dto.PostalCode) ||
                string.IsNullOrWhiteSpace(dto.City))
                throw new ArgumentException("Empty fields are not allowed.");

            var userNameExists = await _userRepo.UserNameExistsAsync(dto.UserName);
            if (userNameExists)
                throw new ArgumentException("Username already taken.");

            var emailExists = await _userRepo.EmailExistsAsync(dto.Email);
            if (emailExists)
                throw new ArgumentException("Email already exists.");

            var password = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = password,
                Address = dto.Address,
                PostalCode = dto.PostalCode,
                City = dto.City
            };

            await _userRepo.AddUserAsync(user);

            return new UserRegisterResponseDto
            {
                Message = "Welcome to E-commerce!",
                UserName = user.UserName,
                Email = user.Email,
                Address = $"{user.Address}, {user.PostalCode}, {user.City}",
                UserNumber = user.UserNumber,
                CreatedAt = user.CreatedAt
            };
        }
    }
}