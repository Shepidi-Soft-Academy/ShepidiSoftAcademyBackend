
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        try
        {
            return await context.SaveChangesAsync(token);
        }
        catch
        {
            throw new Exception("Bir hata meydana geldi.");
        }
    }
}
