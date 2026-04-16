using AutoMapper;
using ShepidiSoft.Application.Features.Courses.Commands.CreateCourse;
using ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Courses;

public sealed class CourseMappingProfile : Profile
{
    public CourseMappingProfile()
    {
        CreateMap<CreateCourseCommand, Course>();
        CreateMap<Course, GetCourseListQueryResponse>();




    }


}
