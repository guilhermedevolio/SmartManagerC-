using SmartManager.API.Response;
using SmartManager.Services.DTOS;
using SmartManager.Services.Requests;

namespace SmartManager.Services.Interfaces
{
    public interface IAuthService {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request); 
        
        Boolean ValidateToken(String token);

        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest tokenPayload);
    }
}