using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Activities.Queries.GetActivityList;

public sealed class GetActivityListQueryHandler(
    IActivityRepository activityRepository,
    IMapper mapper)
    : IRequestHandler<GetActivityListQuery, ServiceResult<List<GetActivityListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetActivityListQueryResponse>>> Handle(
        GetActivityListQuery request,
        CancellationToken cancellationToken)
    {
        var activities = await activityRepository.GetAllAsync();

        var response = mapper.Map<List<GetActivityListQueryResponse>>(activities);

        return ServiceResult<List<GetActivityListQueryResponse>>.Success(response);
    }
}