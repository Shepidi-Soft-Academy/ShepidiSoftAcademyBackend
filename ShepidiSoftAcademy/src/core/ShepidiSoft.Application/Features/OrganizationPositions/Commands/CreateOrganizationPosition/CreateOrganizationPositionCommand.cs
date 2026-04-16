using MediatR;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Commands.CreateOrganizationPosition;

public sealed record CreateOrganizationPositionCommand(
    string Title
    ) : IRequest<ServiceResult<CreateOrganizationPositionCommandResponse>>;
