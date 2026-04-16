using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Queries.GetCoursesAdmin;

public sealed class GetCoursesAdminQuery
    :IRequest<ServiceResult<List<GetCoursesAdminQueryResponse>>>;

