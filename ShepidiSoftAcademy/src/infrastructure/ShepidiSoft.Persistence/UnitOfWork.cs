
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        var result = await context.SaveChangesAsync(token);

        if (result <= 0)
        {
            throw new Exception("Veritabanı işlemleri sırasında bir hata oluştu.");
        }

        return result;
    }
}