using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartManager.Domain.Entities;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace SmartManager.Services.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var expiresAt = DateTime.UtcNow.AddHours(Int32.Parse(_configuration["Auth:Jwt:expiresHours"]));

            var key = Encoding.ASCII.GetBytes(_configuration["Auth:Jwt:key"]);

            var tokenInfo = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(JwtRegisteredClaimNames.Name, user.Email),
                   new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenInfo);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken() {
            using(RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                string token = Convert.ToBase64String(tokenData);

                return token;
            }
        }
    }
}