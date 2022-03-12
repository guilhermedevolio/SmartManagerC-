using SmartManager.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartManger.Infra.Interfaces {
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Remove(long id);
        Task<T> Get(long id);
        Task<List<T>> Get();
    }
}