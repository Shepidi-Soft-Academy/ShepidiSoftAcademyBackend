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

   
}