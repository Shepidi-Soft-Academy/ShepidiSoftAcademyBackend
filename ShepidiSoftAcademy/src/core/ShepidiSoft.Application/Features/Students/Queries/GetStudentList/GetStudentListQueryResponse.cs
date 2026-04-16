namespace ShepidiSoft.Application.Features.Students.Queries.GetStudentList;

public sealed record GetStudentListQueryResponse(Guid Id,
    string Name,
    string Surname);
