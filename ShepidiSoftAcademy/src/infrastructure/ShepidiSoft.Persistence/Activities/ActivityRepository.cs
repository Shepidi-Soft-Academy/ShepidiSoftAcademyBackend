
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Activities;

public sealed class ActivityRepository(AppDbContext context):GenericRepository<Activity,int>(context),IActivityRepository
{
}
