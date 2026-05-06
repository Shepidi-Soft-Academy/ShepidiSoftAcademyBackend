using AutoMapper;
using ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;
using ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.CommunityServices;

public sealed class CommunityServiceMappingProfile : Profile
{
    public CommunityServiceMappingProfile()
    {
        CreateMap<CreateCommunityServiceCommand, CommunityService>();
        CreateMap<CommunityService, GetCommunityServiceListQueryResponse>();

    }


}
