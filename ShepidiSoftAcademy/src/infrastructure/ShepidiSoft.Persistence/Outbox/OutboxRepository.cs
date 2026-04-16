using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Outbox;

public sealed class OutboxRepository(AppDbContext context) : IOutboxRepository
{
    public async Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        await context.OutboxMessages.AddAsync(message, cancellationToken);
    }

    public Task<List<OutboxMessage>> GetPendingAsync(int take = 50, CancellationToken cancellationToken = default)
    {
        return context.OutboxMessages
            .Where(m => !m.IsSent && m.Error == null)
            .OrderBy(m => m.CreatedAt)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
