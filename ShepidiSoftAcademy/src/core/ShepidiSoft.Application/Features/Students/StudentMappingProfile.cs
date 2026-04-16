using AutoMapper;
using ShepidiSoft.Application.Features.Instructors.Commands.CreateInstructor;
using ShepidiSoft.Application.Features.Students.Commands.CreateStudent;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Students;

public sealed class StudentMappingProfile : Profile
{
    public StudentMappingProfile()
    {
        CreateMap<CreateStudentCommand,Student>();

    }


}
