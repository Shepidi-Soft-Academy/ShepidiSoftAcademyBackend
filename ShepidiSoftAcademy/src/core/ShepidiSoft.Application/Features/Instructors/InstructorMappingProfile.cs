using AutoMapper;
using ShepidiSoft.Application.Features.Instructors.Commands.CreateInstructor;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Instructors;

public sealed class InstructorMappingProfile : Profile
{
    public InstructorMappingProfile()
    {
        CreateMap<CreateInstructorCommand, Instructor>();

    }


}
