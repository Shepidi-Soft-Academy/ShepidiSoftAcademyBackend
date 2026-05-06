using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

public sealed class GetCommunityServiceListQueryHandler(
    ICommunityServiceRepository communityServiceRepository,
    IMapper mapper
    ) : IRequestHandler<GetCommunityServiceListQuery, ServiceResult<List<GetCommunityServiceListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetCommunityServiceListQueryResponse>>> Handle(GetCommunityServiceListQuery request, CancellationToken cancellationToken)
    {
       var communityServices = await communityServiceRepository.GetAllAsync();

        if (!communityServices.Any())
            return ServiceResult<List<GetCommunityServiceListQueryResponse>>.Success([]);

        var response = mapper.Map<List<GetCommunityServiceListQueryResponse>>(communityServices);

        return ServiceResult<List<GetCommunityServiceListQueryResponse>>.Success(response);
    }
}
