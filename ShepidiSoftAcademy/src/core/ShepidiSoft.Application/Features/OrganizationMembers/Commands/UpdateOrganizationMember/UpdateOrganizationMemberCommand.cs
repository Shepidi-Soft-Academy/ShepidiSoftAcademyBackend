using MediatR;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Commands.UpdateOrganizationMember;

public sealed record UpdateOrganizationMemberCommand(
    Guid Id,
    IReadOnlyList<int> PositionIds
) : IRequest<ServiceResult>;