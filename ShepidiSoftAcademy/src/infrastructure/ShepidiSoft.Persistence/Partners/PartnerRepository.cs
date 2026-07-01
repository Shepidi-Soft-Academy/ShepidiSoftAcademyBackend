using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Partners;

public sealed class PartnerRepository(AppDbContext context) : GenericRepository<Partner, int>(context), IPartnerRepository
{
    public Task<bool> AnyAsync(Guid id)
    {        
        return context.Set<Partner>().AnyAsync(x => x.PartnerId == id);
    }

    public ValueTask<Partner?> GetByIdAsync(Guid id)
    {       
        return new ValueTask<Partner?>(context.Set<Partner>().FirstOrDefaultAsync(x => x.PartnerId == id));
    }
    public async Task<List<Partner>> GetAllAsync()
    {
        return await context.Set<Partner>().ToListAsync();
    }
}
