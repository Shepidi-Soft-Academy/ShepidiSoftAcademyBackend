using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingList;

public sealed class GetOfferingListQueryHandler(IOfferingRepository offeringRepository,
    IMapper mapper) : IRequestHandler<GetOfferingListQuery, ServiceResult<List<GetOfferingListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetOfferingListQueryResponse>>> Handle(GetOfferingListQuery request, CancellationToken cancellationToken)
    {

        var offerings = await offeringRepository.GetAllAsync();

        var response = mapper.Map<List<GetOfferingListQueryResponse>>(offerings);

        return ServiceResult<List<GetOfferingListQueryResponse>>.Success(response);
    }
}
