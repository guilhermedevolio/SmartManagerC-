using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartManager.Domain.Entities;
using SmartManager.Infra.Context;
using SmartManger.Infra.Interfaces;

namespace SmartManager.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base {
        private readonly SmartManagerContext _context;

        public BaseRepository(SmartManagerContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T obj) {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return  obj;
        }

        public virtual async Task<T> Update(T obj) {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task Remove(long id) {
            var obj = Get(id);

            if(obj != null) {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<T> Get(long id) {
            var obj = await _context.Set<T>()
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .ToListAsync();

            return obj.FirstOrDefault();
        }

        public virtual async Task<List<T>> Get() {
            var obj = await _context.Set<T>()
                            .AsNoTracking()
                            .ToListAsync();
            return obj;
        }

        public virtual async Task<T> FindByExpression(
            Expression<Func<T, bool>> expression,
            bool asNoTracking = true)
                => asNoTracking
                ? await BuildQuery(expression)
                        .AsNoTracking()
                        .FirstOrDefaultAsync()

                : await BuildQuery(expression)
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

        private IQueryable<T> BuildQuery(Expression<Func<T, bool>> expression)
            => _context.Set<T>().Where(expression);

    }
}