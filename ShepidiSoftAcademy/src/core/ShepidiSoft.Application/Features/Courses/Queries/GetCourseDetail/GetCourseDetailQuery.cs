using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Queries.GetCourseDetail;

public sealed record GetCourseDetailQuery(int Id) : IRequest<ServiceResult<GetCourseDetailQueryResponse>>;
