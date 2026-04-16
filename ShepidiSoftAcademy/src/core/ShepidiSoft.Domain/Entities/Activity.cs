
using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Activity : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public bool IsOnline { get; set; }
    public string? Location { get; set; }  // google meet - zoom vs ..
    public string? OnlineMeetingUrl { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
