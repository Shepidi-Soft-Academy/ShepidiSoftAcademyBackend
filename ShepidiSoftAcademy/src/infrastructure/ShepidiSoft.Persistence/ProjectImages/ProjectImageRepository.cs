

using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.ProjectImages;

public sealed class ProjectImageRepository(AppDbContext context) : GenericRepository<ProjectImage, int>(context), IProjectImageRepository
{
}
