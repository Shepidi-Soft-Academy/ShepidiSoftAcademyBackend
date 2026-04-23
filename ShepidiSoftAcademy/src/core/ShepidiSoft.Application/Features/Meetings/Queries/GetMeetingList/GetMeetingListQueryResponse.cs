namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingList;

public sealed record GetMeetingListQueryResponse(
    string Title,
    string MeetingLink,
    DateTime StartTime
    );
