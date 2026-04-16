using AutoMapper;
using ShepidiSoft.Application.Features.Offerings.Commands.CreateOffering;
using ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingDetail;
using ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingList;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Offerings;

public sealed class OfferingMappingProfile : Profile
{
    public OfferingMappingProfile()
    {
        CreateMap<CreateOfferingCommand, Offering>();
        CreateMap<Offering, GetOfferingListQueryResponse>();
        CreateMap<Offering, GetOfferingDetailQueryResponse>();



    }
}
