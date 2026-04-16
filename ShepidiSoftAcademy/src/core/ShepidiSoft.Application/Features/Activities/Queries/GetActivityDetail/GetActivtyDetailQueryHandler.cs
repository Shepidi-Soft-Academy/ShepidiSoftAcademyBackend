using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;

public sealed class GetActivtyDetailQueryHandler(
     IActivityRepository activityRepository,
    IMapper mapper)
    : IRequestHandler<GetActivityDetailQuery, ServiceResult<GetActivityDetailQueryResponse>>
{
    public async Task<ServiceResult<GetActivityDetailQueryResponse>> Handle(GetActivityDetailQuery request, CancellationToken cancellationToken)
    {
        var activity = await activityRepository.GetByIdAsync(request.Id);

        if(activity is null)
            return ServiceResult<GetActivityDetailQueryResponse>.Fail("Aktivite Bulunamadı",HttpStatusCode.NotFound);

        var response = mapper.Map<GetActivityDetailQueryResponse>(activity);

        return ServiceResult<GetActivityDetailQueryResponse>.Success(response);
        

        
    }
}
