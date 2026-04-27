using MediatR;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;

public sealed record CreateCollaborationApplicationCommand(
    
    ):IRequest<ServiceResult<CreateCollaborationApplicationCommandResponse>>;
