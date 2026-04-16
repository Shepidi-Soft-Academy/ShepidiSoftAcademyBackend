using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Project : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = null!; // Proje Başlığı
    public string Description { get; set; } = null!; // Açıklama
    public string? Technologies { get; set; } // Kullanılan Teknolojiler (Örn: .NET 8, React, Docker)
    public string? ImageUrl { get; set; } // Proje Görseli/Kapak Fotoğrafı
    public string? ProjectUrl { get; set; } // Canlı Proje Linki (Web sitesi vs.)
    public bool IsFeatured { get; set; } = false; // Vitrinde/Öne çıkanlarda gösterilsin mi?
    public DateTime? CompletionDate { get; set; } // Tamamlanma Tarihi
    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}