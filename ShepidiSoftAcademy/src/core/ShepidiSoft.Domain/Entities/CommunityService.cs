using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class CommunityService : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsActive { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? UpdatedBy { get; set; }
}
