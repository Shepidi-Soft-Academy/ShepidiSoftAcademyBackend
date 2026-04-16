using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class ProjectImage : BaseEntity<int>, IAuditEntity
{
    public string ImageUrl { get; set; } = null!; // Görselin yolu/linki
    public bool IsMain { get; set; } = false; // Ana/Kapak görseli mi?
    public int OrderIndex { get; set; } // Gösterim sırası
    public string? Description { get; set; } // Görsel altı açıklaması (Tooltip vs.)

    // İlişkiler (Foreign Key)
    public int ProjectId { get; set; }

    // Navigation Property
    public Project Project { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}