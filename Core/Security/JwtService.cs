using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce_Api.Core.Security
{
    public class JwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string TokenGenerator(int id, string role)
        {
            var key = _config["Jwt:Key"];
            if (key == null)
                throw new ArgumentException("Jwt token missing.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddMinutes
                (
                    int.Parse(_config["Jwt:TokenValidityMins"]!)
                ),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}