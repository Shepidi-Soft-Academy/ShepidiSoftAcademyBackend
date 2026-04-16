using MediatR;

namespace ShepidiSoft.Application.Features.Assignments.Commands.CreateAssignment;

public sealed record CreateAssignmentCommand(
    string Title,
    string Description,
    DateTime DueDate,
    int CourseId
) : IRequest<ServiceResult<CreateAssignmentCommandResponse>>;
