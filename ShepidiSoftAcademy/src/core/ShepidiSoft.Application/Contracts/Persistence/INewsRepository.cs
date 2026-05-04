using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface INewsRepository: IGenericRepository<News,int>
{
    Task<News?> GetBySlugAsync(string slug);
    Task<List<News>> GetLatestNewsAsync(int count);
    Task<List<News>> GetNewsWithCategoryAsync();
}
