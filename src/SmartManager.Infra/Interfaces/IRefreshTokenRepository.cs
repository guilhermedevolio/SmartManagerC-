using SmartManager.Domain.Entities;
using SmartManager.Entities;
using SmartManger.Infra.Interfaces;

namespace SmartManager.Infra.Interfaces 
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> getToken(string token);
    }
}