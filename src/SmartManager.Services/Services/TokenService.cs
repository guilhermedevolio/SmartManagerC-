using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartManager.Domain.Entities;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
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

        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

         private TokenValidationParameters GetValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Auth:Jwt:key"]);
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(key) // The same key as the one that generate the token
            };
        }

        public string GenerateRandomToken(int size) {
            using(RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[size];
                rng.GetBytes(tokenData);

                string token = Convert.ToBase64String(tokenData);

                return token;
            }
        }
    }
}