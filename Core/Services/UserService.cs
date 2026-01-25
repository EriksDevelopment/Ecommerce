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
                City = dto.City,
                Role = UserRole.User
            };

            await _userRepo.AddUserAsync(user);

            return new UserRegisterResponseDto
            {
                Message = "Welcome to E-commerce!",
                UserName = user.UserName,
                Email = user.Email,
                Address = $"{user.Address}, {user.PostalCode}, {user.City}",
                UserNumber = user.UserNumber,
                CreatedAt = user.CreatedAt,
            };
        }

        public async Task<UserLoginResponseDto> LoginAsync(UserLoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Empty fields are not allowed.");

            var user = await _userRepo.GetEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");


            var token = _jwt.TokenGenerator(user.Id, user.Role);

            return new UserLoginResponseDto
            {
                Token = token
            };
        }

        public async Task<UserUpdateResponseDto> UpdateAsync(UserUpdateRequestDto dto, int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                throw new ArgumentException("User not found.");

            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                var exists = await _userRepo.UserNameExistsAsync(dto.UserName);
                if (exists)
                    throw new ArgumentException("Username already taken.");

                user.UserName = dto.UserName;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var exists = await _userRepo.UserNameExistsAsync(dto.Email);
                if (exists)
                    throw new ArgumentException("Username already taken.");

                user.Email = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            if (!string.IsNullOrWhiteSpace(dto.Address))
            {
                user.Address = dto.Address;
            }

            if (!string.IsNullOrWhiteSpace(dto.PostalCode))
            {
                user.PostalCode = dto.PostalCode;
            }

            if (!string.IsNullOrWhiteSpace(dto.City))
            {
                user.City = dto.City;
            }

            await _userRepo.UpdateUserAsync(user);

            return new UserUpdateResponseDto
            {
                Message = "Profile updated.",
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                PostalCode = user.PostalCode,
                City = user.City
            };
        }

        public async Task<UserDeleteResponseDto> DeleteAsync(string password, int id)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password can't be empty.");

            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
                throw new ArgumentException("User not found.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid password.");

            await _userRepo.DeleteUserAsync(user);

            return new UserDeleteResponseDto
            {
                Message = $"User {user.UserName} successfully deleted."
            };
        }
    }
}