using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class Partner : BaseEntity<int>, IAuditEntity
{
    public Guid PartnerId { get; set; } = Guid.NewGuid();

    public string Name { get; private set; }
    public string Logo { get; private set; }
    public string WebsiteUrl { get; private set; }
    public void SetLogoUrl(string url) => Logo = url;
    // Audit
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public Partner() { }
}
