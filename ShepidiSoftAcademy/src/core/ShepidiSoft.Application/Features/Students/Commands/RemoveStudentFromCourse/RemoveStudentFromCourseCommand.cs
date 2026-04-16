using MediatR;

namespace ShepidiSoft.Application.Features.Students.Commands.RemoveStudentFromCourse;

public sealed record RemoveStudentFromCourseCommand(int CourseId, Guid UserId) : IRequest<ServiceResult>;