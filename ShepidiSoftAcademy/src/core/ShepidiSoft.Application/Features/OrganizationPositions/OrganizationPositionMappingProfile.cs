using AutoMapper;
using ShepidiSoft.Application.Features.Activities.Commands.CreateActivity;
using ShepidiSoft.Application.Features.OrganizationPositions.Commands.CreateOrganizationPosition;
using ShepidiSoft.Application.Features.OrganizationPositions.Queries.GetOrganizationPositions;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Application.Features.OrganizationPositions;

public sealed class OrganizationPositionMappingProfile : Profile
{
    public OrganizationPositionMappingProfile()
    {
        CreateMap<OrganizationPosition, GetOrganizationPositionsQueryResponse>();
        CreateMap<CreateOrganizationPositionCommand, OrganizationPosition>();


    }
}
