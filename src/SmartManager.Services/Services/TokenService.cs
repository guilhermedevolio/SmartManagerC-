using Microsoft.IdentityModel.Tokens;
using SmartManager.Services.DTOS;
using SmartManager.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SmartManager.Services.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var expiresAt = DateTime.UtcNow.AddHours(2);

            var key = Encoding.ASCII.GetBytes("108923718923679781263791623789126378916837612873651825385");

            var tokenInfo = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim(ClaimTypes.Name, user.Email),
                   new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenInfo);
            return tokenHandler.WriteToken(token);
        }
    }
}