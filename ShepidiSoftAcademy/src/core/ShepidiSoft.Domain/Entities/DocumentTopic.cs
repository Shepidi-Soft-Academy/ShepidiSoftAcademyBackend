
using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class DocumentTopic : BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation Properties
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set ; }
    public Guid? UpdatedBy { get; set; }
}