using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Common;
using ShepidiSoft.Persistence.Context;
using System.Linq.Expressions;

namespace ShepidiSoft.Persistence;

public class GenericRepository<T, TId>(AppDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
    protected AppDbContext Context = context;

    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);
    
    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.AnyAsync(predicate);
    }
    public void Delete(T entity) => _dbSet.Remove(entity);

    public Task<List<T>> GetAllAsync() => _dbSet.ToListAsync();

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    public ValueTask<T?> GetByIdAsync(TId id)=> _dbSet.FindAsync(id);

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) => await _dbSet.CountAsync(predicate);

    public Task<List<TResult>> WhereSelectAsync<TResult>(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken = default)
    {
        return context.Set<T>()
            .Where(predicate)
            .Select(selector)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync()=> await _dbSet.CountAsync();
  
}
