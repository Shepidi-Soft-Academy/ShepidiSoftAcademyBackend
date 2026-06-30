using AutoMapper;
using ShepidiSoft.Application.Features.Organizations.Commands.UpdateOrganization;
using ShepidiSoft.Application.Features.Organizations.Queries.GetOrganizationDetail;
using ShepidiSoft.Domain.Entities.Organizations;

public class OrganizationMappingProfile : Profile
{
    public OrganizationMappingProfile()
    {
        CreateMap<Organization, GetOrganizationDetailQueryResponse>();
        CreateMap<UpdateOrganizationCommand, Organization>();
    }
}