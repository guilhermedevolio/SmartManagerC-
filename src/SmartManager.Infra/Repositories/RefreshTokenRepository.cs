using Microsoft.EntityFrameworkCore;
using SmartManager.Domain.Entities;
using SmartManager.Entities;
using SmartManager.Infra.Context;
using SmartManager.Infra.Interfaces;

namespace SmartManager.Infra.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository {

        private readonly SmartManagerContext _context;

        public RefreshTokenRepository(SmartManagerContext context, IUserRepository userRepository) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken> getToken(string token) {
            var user = await _context.RefreshTokens
                                .Where(x => x.Token == token)
                                .Where(x => x.Revoked == null)
                                .Include(User => User.User)
                                .OrderBy(x => x.Id)
                                .AsNoTracking()
                                .ToListAsync();
                                
            return user.FirstOrDefault();                    
        }

    }
}