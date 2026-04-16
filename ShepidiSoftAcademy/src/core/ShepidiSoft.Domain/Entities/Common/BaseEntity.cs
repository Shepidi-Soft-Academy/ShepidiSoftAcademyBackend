

namespace ShepidiSoft.Domain.Entities.Common;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
}
