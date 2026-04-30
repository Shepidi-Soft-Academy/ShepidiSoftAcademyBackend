namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetMyRequestList;

public sealed record GetMyRequestListQueryResponse(
    string Title,
    string Description,
    string CreatedAt
    );
