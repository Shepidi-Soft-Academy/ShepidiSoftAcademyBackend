using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Organizations;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Organizations;

public sealed class OrganizationRepository(AppDbContext context) : GenericRepository<Organization, int>(context), IOrganizationRepository
{
}
