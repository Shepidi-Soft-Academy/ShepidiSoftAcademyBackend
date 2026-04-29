namespace ShepidiSoft.API.Requests;

public sealed record  UpdateMeetingRequest(
    string Title,
    string Description,
    DateTime StartTime,
    string MeetingLink
    );
