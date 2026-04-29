namespace ShepidiSoft.Application.Features.StudentRequests.Queries;

//sadece gereklı alanları doner
public sealed record GetStudentRequestListQueryResponse(
    Guid Id,
    string Title,
    string Description,
    string StudentRequestStatus, // Enum'dan string'e dönecek
    DateTime Created);