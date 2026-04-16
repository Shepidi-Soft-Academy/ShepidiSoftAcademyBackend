using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities.Organizations;

public sealed class OrganizationPosition : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public ICollection<OrganizationMemberPosition> Members { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
