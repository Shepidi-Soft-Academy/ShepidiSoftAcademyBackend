
using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Course : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int InstructorId { get; set; }
    public string Location { get; set; } = null!;
    public bool IsOnline { get; set; }
    public string? MeetingUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public int DurationInWeeks { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = null!;
    public decimal? Price { get; set; }
    public string? ApplicationFormUrl { get; set; }


    //// Relations

    // 1 Course -> 1 Instructor
    public Instructor Instructor { get; set; } = null!;

    //// 1 Course -> N Assignments
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    // N Course -> N Student
    public ICollection<Student> Students { get; set; } = new List<Student>();

    public ICollection<CourseMembership> Memberships { get; set; } = new List<CourseMembership>();
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
