using System.Linq.Expressions;
using LinqKit;
using SmartManager.Domain.Entities;
using SmartManger.Infra.Interfaces;

namespace SmartManager.Infra.Interfaces 
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<List<User>> AdvancedSearch(Expression<Func<User, Boolean>> pb);
    }
}