using MediatR;

namespace ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

public sealed record GetCommunityServiceListQuery() : IRequest<ServiceResult<List<GetCommunityServiceListQueryResponse>>>
{
}
