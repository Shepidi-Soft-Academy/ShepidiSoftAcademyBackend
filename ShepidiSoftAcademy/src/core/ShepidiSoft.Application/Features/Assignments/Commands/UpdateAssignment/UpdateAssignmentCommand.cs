using MediatR;

namespace ShepidiSoft.Application.Features.Assignments.Commands.UpdateAssignment;

public sealed record UpdateAssignmentCommand(
    int Id,
    string Title,
    string Description,
    DateTime DueDate,
    int CourseId
    ) : IRequest<ServiceResult>;
