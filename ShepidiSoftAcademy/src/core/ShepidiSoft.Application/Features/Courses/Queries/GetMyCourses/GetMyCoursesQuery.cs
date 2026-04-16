using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses;

public sealed record GetMyCoursesQuery
  : IRequest<ServiceResult<List<GetCourseListQueryResponse>>>;
