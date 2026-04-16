using ShepidiSoft.Domain.Entities.Common;
using ShepidiSoft.Domain.Entities.Enums;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Domain.Entities;

public sealed class CareerApplication : BaseEntity<int>, IAuditEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;

    public string? PhoneNumber { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? GithubUrl { get; set; }

    public string CoverLetter { get; set; } = default!;
    public string? CvUrl { get; set; }

    public int OrganizationPositionId { get; set; }
    public OrganizationPosition OrganizationPosition { get; set; }

    public ApplicationStatus Status { get; set; } // Pending, Approved, Rejected

    public string? AdminNote { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? UpdatedBy { get; set ; }
}
