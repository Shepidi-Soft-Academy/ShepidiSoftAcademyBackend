using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Partners.Queries.GetAllPartners;

public sealed class GetAllPartnersHandler(IPartnerRepository partnerrepository, IMapper mapper)
    :IRequestHandler <GetAllPartnersQuery, ServiceResult<List<GetAllPartnersResponse>>>
{
    public async Task<ServiceResult<List<GetAllPartnersResponse>>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
    {


        var partners = await partnerrepository.GetAllAsync();

        var response = partners.Select(x => new GetAllPartnersResponse(x.Id, x.Name, x.Logo, x.WebsiteUrl)).ToList();


        return ServiceResult<List<GetAllPartnersResponse>>.Success(response);
    }
}
