
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken token) => context.SaveChangesAsync(token);

}