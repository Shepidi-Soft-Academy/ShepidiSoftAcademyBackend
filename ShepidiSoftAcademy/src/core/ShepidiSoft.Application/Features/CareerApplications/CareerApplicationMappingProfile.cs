using AutoMapper;
using ShepidiSoft.Application.Features.CareerApplications.Commands.CreateCareerApplication;
using ShepidiSoft.Application.Features.Courses.Commands.CreateCourse;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.CareerApplications;

public sealed class CareerApplicationMappingProfile : Profile
{
    public CareerApplicationMappingProfile()
    {
        CreateMap<CreateCareerApplicationCommand, CareerApplication>();

    }
}
