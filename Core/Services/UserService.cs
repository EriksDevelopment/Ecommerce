using System.ComponentModel.DataAnnotations;
using Ecommerce_Api.Core.Interfaces;
using Ecommerce_Api.Core.Security;
using Ecommerce_Api.Data.Dtos.User;
using Ecommerce_Api.Data.Interfaces;
using Ecommerce_Api.Data.Models;

namespace Ecommerce_Api.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly JwtService _jwt;
        public UserService(IUserRepo userRepo, JwtService jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
        }

        public async Task<UserRegisterResponseDto> AddAsync(UserRegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
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

            var password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

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

        public async Task<UserLoginResponseDto> LoginAsync(UserLoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Empty fields are not allowed.");

            var email = await _userRepo.GetEmailAsync(dto.Email);

            if (email == null || !BCrypt.Net.BCrypt.Verify(dto.Password, email.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            var token = _jwt.TokenGenerator(email.Id, "User");

            return new UserLoginResponseDto
            {
                Token = token
            };
        }
    }
}