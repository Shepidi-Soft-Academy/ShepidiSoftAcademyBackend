using AutoMapper;
using ShepidiSoft.Application.Features.Assignments.Commands.CreateAssignment;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Assignments;

public sealed class AssignmentMappingProfile : Profile
{
    public AssignmentMappingProfile()
    {
        CreateMap<CreateAssignmentCommand, Assignment>();


    }
}
