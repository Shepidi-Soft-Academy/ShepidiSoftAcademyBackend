using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.CollaborationApplications;

public sealed class CollaborationApplicationRepository(AppDbContext context) : GenericRepository<CollaborationApplication, int>(context), ICollaborationApplicationRepository
{
}
