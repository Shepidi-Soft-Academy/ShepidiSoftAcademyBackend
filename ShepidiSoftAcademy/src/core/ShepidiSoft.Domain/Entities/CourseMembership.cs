using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities
{
    public sealed class CourseMembership : BaseEntity<int>, IAuditEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid UserId { get; set; }  // Identity UserId
        public string Role { get; set; } = null!; // "Student", "Instructor", "Assistant" ...

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Audit
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
