using MediatR;

namespace ShepidiSoft.Application.Features.Students.Commands.DeleteStudent;

public sealed record DeleteStudentCommand(Guid Id) : IRequest<ServiceResult>;
