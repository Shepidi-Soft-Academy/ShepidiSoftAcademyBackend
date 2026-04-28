using MediatR;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.DeleteCollaborationApplication;

public sealed record DeleteCollaborationApplicationCommand(int Id) : IRequest<ServiceResult>;
