namespace ShepidiSoft.API.Requests
{
    public sealed record UpdateNewsRequest(
        string Title,
        string Content,
        string? Summary,
        string? ThumbnailUrl,
        string? BannerUrl,
        bool IsPublished
        );
}
