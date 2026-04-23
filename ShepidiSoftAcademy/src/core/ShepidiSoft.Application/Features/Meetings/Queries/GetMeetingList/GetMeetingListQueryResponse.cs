namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingList;

public sealed record GetMeetingListQueryResponse(
    int Id,
    string Title,
    string MeetingLink,
    DateTime StartTime
    );
