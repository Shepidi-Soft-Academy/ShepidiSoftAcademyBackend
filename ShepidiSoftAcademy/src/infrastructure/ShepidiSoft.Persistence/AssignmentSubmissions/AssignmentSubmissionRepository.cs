using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.AssignmentSubmissions;

public sealed class AssignmentSubmissionRepository(AppDbContext context) : GenericRepository<AssignmentSubmission, int>(context), IAssignmentSubmissionRepository
{
}
