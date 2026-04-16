using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Organizations;
using ShepidiSoft.Persistence.Context;


namespace ShepidiSoft.Persistence.OrganizationMembers;

public sealed class OrganizationMemberRepository(AppDbContext context) : GenericRepository<OrganizationMember, Guid>(context), IOrganizationMemberRepository
{
    public Task<List<OrganizationMember>> GetAllWithPositionsAsync(CancellationToken cancellationToken)
      => context.OrganizationMembers
          .Include(x => x.Positions)
          .ThenInclude(x => x.OrganizationPosition)
          .ToListAsync(cancellationToken);

    public Task<OrganizationMember?> GetByIdWithPositionsAsync(Guid id, CancellationToken cancellationToken)
       => context.OrganizationMembers
           .Include(x => x.Positions)
           .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}
