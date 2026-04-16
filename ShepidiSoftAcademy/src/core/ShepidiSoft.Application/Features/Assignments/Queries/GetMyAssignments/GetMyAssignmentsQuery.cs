using MediatR;

namespace ShepidiSoft.Application.Features.Assignments.Queries.GetMyAssignments;
public sealed record GetMyAssignmentsQuery : IRequest<ServiceResult<List<GetMyAssignmentsQueryResponse>>>
{
    public Guid? StudentId { get; init; }
    public Guid? InstructorId { get; init; }
    public string Role { get; init; } = null!;
}