using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;
using System.Linq.Expressions;

namespace ShepidiSoft.Persistence.Newss;

public sealed class NewsRepository(AppDbContext context) : GenericRepository<News, int>(context), INewsRepository
{
    // Slug üzerinden haber getir
    public async Task<News?> GetBySlugAsync(string slug)
    {
        return await context.Set<News>()
            .FirstOrDefaultAsync(x => x.Slug == slug);
    }

    // Son haberleri getir
    public async Task<List<News>> GetLatestNewsAsync(int count)
    {
        return await context.Set<News>()
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.PublishedAt)
            .Take(count)
            .ToListAsync();
    }

    // Kategorisiyle birlikte haberleri getir (Ekinlik/Kategori varsa)
    public async Task<List<News>> GetNewsWithCategoryAsync()
    {
        return await context.Set<News>().ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<News, bool>> predicate) =>
        await context.Set<News>().AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<News, bool>> predicate) =>
        await context.Set<News>().CountAsync(predicate);

    public IQueryable<News> Where(Expression<Func<News, bool>> predicate) =>
        context.Set<News>().Where(predicate);

    public async Task<List<News>> GetAllAsync() =>
        await context.Set<News>().ToListAsync();

    public async Task<List<News>> GetAllPagedAsync(int pageNumber, int pageSize) =>
        await context.Set<News>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async ValueTask<News?> GetByIdAsync(int id) =>
        await context.Set<News>().FindAsync(id);

    public async ValueTask AddAsync(News entity) =>
        await context.Set<News>().AddAsync(entity);

    public void Update(News entity) =>
        context.Set<News>().Update(entity);

    public void Delete(News entity) =>
        context.Set<News>().Remove(entity);

    public async Task<List<TResult>> WhereSelectAsync<TResult>(
        Expression<Func<News, bool>> predicate,
        Expression<Func<News, TResult>> selector,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<News>()
            .Where(predicate)
            .Select(selector)
            .ToListAsync(cancellationToken);
    }
}