using MediatR;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;

public sealed record CreateCommunityServiceCommand(
    string Title,
    string Description,
    bool IsActive,
    string? ImageUrl
    ) : IRequest<ServiceResult<CreateCommunityServiceCommandResponse>>;
