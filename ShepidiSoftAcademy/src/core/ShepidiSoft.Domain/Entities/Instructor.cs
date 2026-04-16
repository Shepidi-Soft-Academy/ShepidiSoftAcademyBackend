using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Instructor : BaseEntity<int>, IAuditEntity
{
    public Guid UserId { get; set; } 
    public string Bio { get; set; } = null!;
    public string Expertise { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    // Navigation Properties  
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}