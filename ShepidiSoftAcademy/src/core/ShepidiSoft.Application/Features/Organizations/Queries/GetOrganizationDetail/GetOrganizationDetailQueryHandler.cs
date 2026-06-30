using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Organizations.Queries.GetOrganizationDetail;

public sealed class GetOrganizationDetailQueryHandler(
    IOrganizationRepository organizationRepository,
    IMapper mapper
) : IRequestHandler<GetOrganizationDetailQuery, ServiceResult<GetOrganizationDetailQueryResponse>>
{
    public async Task<ServiceResult<GetOrganizationDetailQueryResponse>> Handle(
      GetOrganizationDetailQuery request,
      CancellationToken cancellationToken)
    {
       
        var organization = await organizationRepository.GetByIdAsync(request.Id);

        if (organization is null)
        {
            return ServiceResult<GetOrganizationDetailQueryResponse>
                .Fail("Organization bulunamadı.", HttpStatusCode.NotFound);
        }

        var response = mapper.Map<GetOrganizationDetailQueryResponse>(organization);
        return ServiceResult<GetOrganizationDetailQueryResponse>.Success(response);
    }
}