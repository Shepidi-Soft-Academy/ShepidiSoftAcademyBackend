namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingDetail;

public sealed record GetMeetingDetailQueryResponse(
    string Title,
    string Description,
    string MeetingLink,
    DateTime StartTime,
    string CreatedByName
    );