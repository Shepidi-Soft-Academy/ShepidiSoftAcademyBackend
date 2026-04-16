using AutoMapper;
using ShepidiSoft.Application.Features.OrganizationMembers.Commands.CreateOrganizationMember;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Application.Features.OrganizationMembers;

public sealed class OrganizationMemberMappingProfile : Profile
{
    public OrganizationMemberMappingProfile()
    {
        CreateMap<CreateOrganizationMemberCommand, OrganizationMember>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Positions, opt => opt.MapFrom(src =>
                src.PositionIds.Select(id => new OrganizationMemberPosition
                {
                    OrganizationPositionId = id
                }).ToList()));
    }
}