using SmartManager.API.Response;
using SmartManager.Services.DTOS;

namespace SmartManager.Services.Interfaces
{
    public interface ITokenService {
        String GenerateToken(UserDTO user);
    }
}