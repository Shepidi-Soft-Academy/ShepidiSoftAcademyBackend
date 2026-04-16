using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class ContactMessage : BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = null!;      // Göndericinin Adı Soyadı
    public string Email { get; set; } = null!;     // E-posta Adresi
    public string? Phone { get; set; }             // Telefon Numarası (Opsiyonel olabilir)
    public string Content { get; set; } = null!;   // Mesaj İçeriği
    public DateTime SentAt { get; set; } = DateTime.UtcNow; // Gönderilme Zamanı
    public bool IsRead { get; set; } = false;      // Okundu bilgisi
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}