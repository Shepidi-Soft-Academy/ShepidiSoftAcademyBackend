using MediatR;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Commands.DeleteOrganizationMember;

public sealed record DeleteOrganizationMemberCommand(
    Guid Id
    ) : IRequest<ServiceResult>;
