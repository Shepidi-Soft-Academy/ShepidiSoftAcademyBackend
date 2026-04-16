using MediatR;

namespace ShepidiSoft.Application.Features.Instructors.Commands.DeleteInstructor;

public sealed record DeleteInstructorCommand(int Id) : IRequest<ServiceResult>;


