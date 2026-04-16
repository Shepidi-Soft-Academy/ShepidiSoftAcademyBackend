using System.Linq.Expressions;

namespace ShepidiSoft.Application.Contracts.Persistence;


public interface IGenericRepository<T, TId> where T : class where TId : struct
{
    Task<bool> AnyAsync(TId id);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync();

    IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    ValueTask<T?> GetByIdAsync(TId id);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<List<TResult>> WhereSelectAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken = default);
}
