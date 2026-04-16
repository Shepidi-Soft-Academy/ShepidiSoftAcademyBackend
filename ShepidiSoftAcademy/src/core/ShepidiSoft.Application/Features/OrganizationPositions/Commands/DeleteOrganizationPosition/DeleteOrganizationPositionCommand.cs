using MediatR;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Commands.DeleteOrganizationPosition;

public sealed record DeleteOrganizationPositionCommand(
    int Id
    ) : IRequest<ServiceResult>;
