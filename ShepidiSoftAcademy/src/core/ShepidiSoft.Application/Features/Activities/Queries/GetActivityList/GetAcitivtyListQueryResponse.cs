namespace ShepidiSoft.Application.Features.Activities.Queries.GetActivityList;

public sealed record GetActivityListQueryResponse(
    int Id,         
    string Title,
    DateTime Date,
    bool IsOnline,
    string? Location
);