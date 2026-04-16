namespace ShepidiSoft.API;

public sealed record UpdateAssignmentRequest(
    string Title,
    string Description,
    DateTime DueDate,
    int CourseId);
