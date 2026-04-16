using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Student : BaseEntity<Guid>, IAuditEntity
{
    public Guid UserId { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public string University { get; set; } = default!;
    public string Department { get; set; } = default!;
    
    // Navigation Properties
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}