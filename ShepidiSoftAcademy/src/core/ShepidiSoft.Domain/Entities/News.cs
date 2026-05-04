using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities;

public sealed class News : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Summary { get; set; }
    public string Slug { get; set; } = null!;
    public string?  ThumbnailUrl { get; set; }
    public string? BannerUrl { get; set; }
    public int ViewCount { get; set; } = 0;
    public bool IsPublished { get; set; } = false;
    public DateTime PublishedAt { get; set; }
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? UpdatedBy { get; set; }
}