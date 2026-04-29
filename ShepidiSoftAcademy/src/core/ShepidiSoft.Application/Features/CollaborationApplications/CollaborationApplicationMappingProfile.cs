using AutoMapper;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;
using ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationDetail;
using ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationList;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.CollaborationApplications;

public sealed class CollaborationApplicationMappingProfile : Profile
{
    public CollaborationApplicationMappingProfile()
    {
        CreateMap<CreateCollaborationApplicationCommand,CollaborationApplication>();
        CreateMap<CollaborationApplication, GetCollaborationApplicationListQueryResponse>();
        CreateMap<CollaborationApplication, GetCollaborationApplicationDetailQueryResponse>();


    }


}
