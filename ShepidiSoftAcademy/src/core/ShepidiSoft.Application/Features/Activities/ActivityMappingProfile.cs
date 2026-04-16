using AutoMapper;
using ShepidiSoft.Application.Features.Activities.Commands.CreateActivity;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityList;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Activities;

public sealed class ActivityMappingProfile : Profile
{
    public ActivityMappingProfile()
    {
        CreateMap<CreateActivityCommand, Activity>();

        CreateMap<Activity, GetActivityListQueryResponse>();
        CreateMap<Activity, GetActivityDetailQueryResponse>();

    }
}