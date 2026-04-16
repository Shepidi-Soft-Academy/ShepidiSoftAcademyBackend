using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities.Organizations;

public sealed class OrganizationMemberPosition : IAuditEntity
{
    public Guid OrganizationMemberId { get; set; }
    public OrganizationMember OrganizationMember { get; set; }
    public int OrganizationPositionId { get; set; }
    public OrganizationPosition OrganizationPosition { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get ; set ; }
    public Guid? UpdatedBy { get; set; }
}
