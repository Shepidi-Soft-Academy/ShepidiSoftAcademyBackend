using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Organizations;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.OrganizationPositions;

public sealed class OrganizationPositionRepository(AppDbContext context) : GenericRepository<OrganizationPosition, int>(context), IOrganizationPositionRepository
{
}
