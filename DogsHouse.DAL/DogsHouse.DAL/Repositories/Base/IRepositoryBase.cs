using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DogsHouse.DAL.Repositories.Base
{
    public interface IRepositoryBase<T>
   where T : class
    {
        IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = default);

        Task<T> CreateAsync(T entity);

        T Create(T entity);

        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
            Expression<Func<T, T>>? selector = default);

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = default,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
            Expression<Func<T, T>>? selector = default);

        IQueryable<T> GetQueryable(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
           Expression<Func<T, T>>? selector = default);
    }
}
