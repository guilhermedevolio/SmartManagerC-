using Microsoft.EntityFrameworkCore;
using SmartManager.Domain.Entities;
using SmartManager.Infra.Context;
using SmartManager.Infra.Interfaces;

namespace SmartManager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository {

        private readonly SmartManagerContext _context;

        public UserRepository(SmartManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email) {
            var user = await _context.Users
                                .Where(x => x.Email == email)
                                .AsNoTracking()
                                .ToListAsync();
                                
            return user.FirstOrDefault();                    
        }
    }
}