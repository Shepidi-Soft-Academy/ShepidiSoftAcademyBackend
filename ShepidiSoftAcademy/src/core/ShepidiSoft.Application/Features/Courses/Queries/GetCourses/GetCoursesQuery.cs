using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Queries.GetCourses
{
    public sealed record GetCoursesQuery
     : IRequest<ServiceResult<List<GetCoursesQueryResponse>>>;

}
