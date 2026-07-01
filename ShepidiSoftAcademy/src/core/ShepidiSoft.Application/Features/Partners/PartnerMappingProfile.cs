using AutoMapper;
using ShepidiSoft.Application.Features.Partners.Commands.CreatePartner;
using ShepidiSoft.Application.Features.Partners.Queries.GetAllPartners;
using ShepidiSoft.Domain.Entities;


namespace ShepidiSoft.Application.Features.Partners;

public sealed class PartnerMappingProfile :Profile
{
    public PartnerMappingProfile()
    {
        CreateMap<CreatePartnerCommand, Partner>();

        CreateMap<Partner, GetAllPartnersResponse>();

    }
}
