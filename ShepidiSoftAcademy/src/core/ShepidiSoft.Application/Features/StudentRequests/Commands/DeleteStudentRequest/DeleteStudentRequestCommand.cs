using MediatR;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.DeleteStudentRequest;

public sealed record DeleteStudentRequestCommand(Guid Id) : IRequest<ServiceResult<string>>;