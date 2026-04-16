namespace ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;

public sealed record GetActivityDetailQueryResponse(
    int Id,
    string Title,
    string Description,
    DateTime Date,
    bool IsOnline,
    string Location,
    string OnlineMeetingUrl
    );

