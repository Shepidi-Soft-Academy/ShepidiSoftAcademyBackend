using MediatR;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.UpdateCommunityService;

public sealed record UpdateCommunityServiceCommand(
    int Id,
    string Title,
    string Description,
    bool IsActive,
    string? ImageUrl
    ) : IRequest<ServiceResult>;
