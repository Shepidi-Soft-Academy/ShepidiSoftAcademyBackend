using MediatR;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;

public sealed record CreateCollaborationApplicationCommand(
    string Title,
    string Description,
    string CommunityName,
    string ContactName,
    string ContactEmail,
    string ContactPhone
    ) :IRequest<ServiceResult<CreateCollaborationApplicationCommandResponse>>;
