using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class AssignmentSubmission : BaseEntity<int>, IAuditEntity
{
    public string SubmissionContent { get; set; } = null!; // Dosya linki veya metin cevabı
    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;
    public string? InstructorFeedback { get; set; } // Eğitmen geri bildirimi
    public decimal? Grade { get; set; } // Notlandırma (Henüz notlanmamış olabilir)

    // İlişkiler (Foreign Keys)
    public int AssignmentId { get; set; }
    public Guid StudentId { get; set; }

    // Navigation Properties
    public Assignment Assignment { get; set; } = null!;
    public Student Student { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}