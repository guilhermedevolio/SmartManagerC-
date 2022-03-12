using SmartManager.Domain.Entities;

namespace SmartManager.Infra.Interfaces 
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}