using MediatR;

namespace ShepidiSoft.Application.Features.Newss.Commands.CreateNewss;

public sealed record CreateNewsCommand(
    string Title,
    string Content,
    string Summary,
    string? ThumbnailUrl,
    string? BannerUrl,
    bool IsPublished
    ) : IRequest<ServiceResult<CreateNewsCommandResponse>>;
