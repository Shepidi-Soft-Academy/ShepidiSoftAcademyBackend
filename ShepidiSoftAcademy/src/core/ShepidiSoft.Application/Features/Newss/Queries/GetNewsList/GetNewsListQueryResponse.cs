namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsList;

public sealed record GetNewsListQueryResponse(
    int Id,
    string Title,
    string? Summary,
    string Slug,
    string? ThumbnailUrl,
    DateTime PublishedAt,
    int ViewCount
);
