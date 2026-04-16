using ShepidiSoft.Domain.Entities.Common;


namespace ShepidiSoft.Domain.Entities;

public sealed class Document : BaseEntity<int>,IAuditEntity
{
    public int DocumentTopicId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string FileUrl { get; set; } = null!;
    public DateTime PublishedAt { get; set; }
    public string UploadedByUserId { get; set; } = null!;

    // Navigation Properties
    public DocumentTopic DocumentTopic { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
