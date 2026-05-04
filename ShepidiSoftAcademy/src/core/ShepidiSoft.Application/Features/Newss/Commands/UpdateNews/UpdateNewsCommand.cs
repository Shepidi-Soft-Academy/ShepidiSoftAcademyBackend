using MediatR;

namespace ShepidiSoft.Application.Features.Newss.Commands.UpdateNewss;

public sealed record UpdateNewsCommand(
    int Id,
    string Title,
    string Content,
    string Summary,
    string? ThumbnailUrl,
    string? BannerUrl,
    bool IsPublished
    ) : IRequest<ServiceResult>;
