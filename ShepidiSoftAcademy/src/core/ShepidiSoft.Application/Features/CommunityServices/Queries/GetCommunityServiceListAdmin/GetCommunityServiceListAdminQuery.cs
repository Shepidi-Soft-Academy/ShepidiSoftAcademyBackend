using MediatR;

namespace ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

public sealed record GetCommunityServiceListAdminQuery():IRequest<ServiceResult<List<GetCommunityServiceListAdminQueryResponse>>>;
