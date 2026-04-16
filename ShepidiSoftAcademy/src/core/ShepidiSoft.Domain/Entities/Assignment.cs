using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Assignment : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; } // Teslim tarihi 
    public bool IsActive { get; set; } = true; // Aktif/Pasif yönetimi için

    // Foreign Key: 1 Assignment -> 1 Course
    public int CourseId { get; set; }

    // Navigation Properties
    public Course Course { get; set; } = null!;

    // 1 Assignment -> N Submissionsniz)
     public ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>();
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}