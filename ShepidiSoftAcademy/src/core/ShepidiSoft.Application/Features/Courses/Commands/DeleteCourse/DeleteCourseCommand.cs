using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Commands.DeleteCourse;

public sealed record DeleteCourseCommand(int Id) : IRequest<ServiceResult>;
