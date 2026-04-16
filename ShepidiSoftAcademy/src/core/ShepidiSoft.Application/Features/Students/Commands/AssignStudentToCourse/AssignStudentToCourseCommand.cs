using MediatR;

namespace ShepidiSoft.Application.Features.Students.Commands.AssignStudentToCourse;

public sealed record AssignStudentToCourseCommand(Guid StudentId, int CourseId)
  : IRequest<ServiceResult>;