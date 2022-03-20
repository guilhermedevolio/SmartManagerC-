using SmartManager.API.Response;
using SmartManager.Domain.Entities;
using SmartManager.Services.DTOS;

namespace SmartManager.Services.Interfaces
{
    public interface ITokenService {
        String GenerateToken(User user);
        Boolean ValidateToken(string token);

        String GenerateRandomToken(int size);
    }
}