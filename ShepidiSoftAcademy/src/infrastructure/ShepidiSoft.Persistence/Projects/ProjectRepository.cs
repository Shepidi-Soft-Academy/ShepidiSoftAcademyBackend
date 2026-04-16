using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Projects;

public sealed class ProjectRepository(AppDbContext context) : GenericRepository<Project, int>(context), IProjectRepository
{
}
