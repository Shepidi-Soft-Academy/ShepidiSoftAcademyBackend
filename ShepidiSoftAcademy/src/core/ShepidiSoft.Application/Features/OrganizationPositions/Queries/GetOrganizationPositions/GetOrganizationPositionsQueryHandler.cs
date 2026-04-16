using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityList;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Queries.GetOrganizationPositions;

public sealed class GetOrganizationPositionsQueryHandler(
    IOrganizationPositionRepository organizationPositionRepository,
    IMapper mapper

    ) : IRequestHandler<GetOrganizationPositionsQuery, ServiceResult<List<GetOrganizationPositionsQueryResponse>>>
{
    public async Task<ServiceResult<List<GetOrganizationPositionsQueryResponse>>> Handle(GetOrganizationPositionsQuery request, CancellationToken cancellationToken)
    {

        var organizationPositions = await organizationPositionRepository.GetAllAsync();

        var response = mapper.Map<List<GetOrganizationPositionsQueryResponse>>(organizationPositions);

        return ServiceResult<List<GetOrganizationPositionsQueryResponse>>.Success(response);
    }
}

