using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingDetail;

public sealed class GetOfferingDetailQueryHandler(IOfferingRepository offeringRepository,
    IMapper mapper) : IRequestHandler<GetOfferingDetailQuery, ServiceResult<GetOfferingDetailQueryResponse>>
{
    public async Task<ServiceResult<GetOfferingDetailQueryResponse>> Handle(GetOfferingDetailQuery request, CancellationToken cancellationToken)
    {
        var offering = await offeringRepository.GetByIdAsync(request.Id);

        if (offering is null)
            return ServiceResult<GetOfferingDetailQueryResponse>.Fail("Hizmet Bulunamadı", HttpStatusCode.NotFound);

        var response = mapper.Map<GetOfferingDetailQueryResponse>(offering);

        return ServiceResult<GetOfferingDetailQueryResponse>.Success(response);

    }
}
