using MediatR;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.UpdateCollaborationApplicationStatus;

public sealed record UpdateCollaborationApplicationStatusCommand(
    int Id,
    CollaborationApplicationStatus Status
    ) : IRequest<ServiceResult>;
