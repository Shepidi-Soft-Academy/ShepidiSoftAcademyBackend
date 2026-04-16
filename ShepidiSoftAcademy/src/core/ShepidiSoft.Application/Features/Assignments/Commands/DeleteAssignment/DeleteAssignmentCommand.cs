using MediatR;

namespace ShepidiSoft.Application.Features.Assignments.Commands.DeleteAssignment;

public sealed record DeleteAssignmentCommand(int Id) : IRequest<ServiceResult>;
