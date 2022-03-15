using SmartManager.API.Response;
using SmartManager.Domain.Entities;
using SmartManager.Services.DTOS;

namespace SmartManager.Services.Interfaces
{
    public interface ITokenService {
        String GenerateToken(User user);
        String GenerateRefreshToken();
    }
}