namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsDetail;

public sealed record GetNewsDetailQueryResponse(
    int Id,
    string Title,
    string Content,
    string? Summary,
    string Slug,
    string? ThumbnailUrl,
    string? BannerUrl,
    int ViewCount,
    DateTime PublishedAt,
    DateTime Created
);