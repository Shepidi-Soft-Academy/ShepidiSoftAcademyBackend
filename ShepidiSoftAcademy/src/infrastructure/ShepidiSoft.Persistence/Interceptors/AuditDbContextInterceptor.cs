using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Persistence.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditDbContextInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity, Guid?>> Behaviors = new()
        {
            { EntityState.Added, AddBehavior },
            { EntityState.Modified, ModifiedBehavior }
        };

        private static void AddBehavior(DbContext context, IAuditEntity auditEntity, Guid? userId)
        {
            auditEntity.Created = DateTime.UtcNow;
            auditEntity.CreatedBy = userId;
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
            context.Entry(auditEntity).Property(x => x.UpdatedBy).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity, Guid? userId)
        {
            auditEntity.Updated = DateTime.UtcNow;
            auditEntity.UpdatedBy = userId;
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
            context.Entry(auditEntity).Property(x => x.CreatedBy).IsModified = false;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId; 

            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;
                if (entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;

                Behaviors[entityEntry.State](eventData.Context, auditEntity, userId);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}