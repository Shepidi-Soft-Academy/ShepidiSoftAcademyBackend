using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Announcement : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishedAt { get; set; }
    public string CreatedByUserId { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}