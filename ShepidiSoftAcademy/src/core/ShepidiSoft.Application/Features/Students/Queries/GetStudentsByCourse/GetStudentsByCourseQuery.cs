using MediatR;
using ShepidiSoft.Application;

public sealed record GetStudentsByCourseQuery(int CourseId):IRequest<ServiceResult<List<GetStudentsByCourseQueryResponse>>>;
    
