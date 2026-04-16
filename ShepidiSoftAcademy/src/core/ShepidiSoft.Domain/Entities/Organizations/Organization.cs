using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities.Organizations;

public sealed class Organization : BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? LogoUrl { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
