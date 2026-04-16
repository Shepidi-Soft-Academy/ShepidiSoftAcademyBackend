namespace ShepidiSoft.Application.Features.Assignments.Queries.GetMyAssignments;

public sealed record GetMyAssignmentsQueryResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime DueDate { get; init; }
    public bool IsActive { get; init; }

    // Course bilgisi
    public string CourseName { get; init; } = null!;
}