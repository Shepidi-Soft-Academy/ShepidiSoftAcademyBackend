using MediatR;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Queries.GetOrganizationMemberList;

public sealed record GetOrganizationMemberListQuery() : IRequest<ServiceResult<List<GetOrganizationMemberListQueryResponse>>>;
