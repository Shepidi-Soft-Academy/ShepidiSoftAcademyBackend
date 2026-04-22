using ShepidiSoft.Domain.Entities.Common;

public sealed class Meeting : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string MeetingLink { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? UpdatedBy { get; set; }
}