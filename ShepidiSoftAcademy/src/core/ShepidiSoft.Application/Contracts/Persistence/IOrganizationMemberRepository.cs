
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface IOrganizationMemberRepository : IGenericRepository<OrganizationMember, Guid>
{
    Task<List<OrganizationMember>> GetAllWithPositionsAsync(CancellationToken cancellationToken);
    Task<OrganizationMember?> GetByIdWithPositionsAsync(Guid id, CancellationToken cancellationToken);
}