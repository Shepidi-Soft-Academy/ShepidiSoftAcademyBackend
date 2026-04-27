using AutoMapper;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.CollaborationApplications;

public sealed class CollaborationApplicationMappingProfile : Profile
{
    public CollaborationApplicationMappingProfile()
    {
        CreateMap<CreateCollaborationApplicationCommand,CollaborationApplication>();
    }

   
}
