using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Common;

public sealed class Student : BaseEntity<Guid>, IAuditEntity
{
    public Guid UserId { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public string University { get; set; } = default!;
    public string Department { get; set; } = default!;

    // Navigation Properties
    public ICollection<StudentRequest> StudentRequests { get; set; } = new List<StudentRequest>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = new List<AssignmentSubmission>();

    // Audit
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}