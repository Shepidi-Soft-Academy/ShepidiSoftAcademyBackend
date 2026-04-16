using MediatR;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Commands.CreateOrganizationMember;

public sealed record CreateOrganizationMemberCommand(
    Guid? UserId,
    CreateUserRequest? CreateUserRequest,
    IReadOnlyList<int> PositionIds
) : IRequest<ServiceResult<Guid>>;