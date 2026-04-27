namespace ShepidiSoft.API;

public sealed record  UpdateMeetingRequest(
    string Title,
    string Description,
    DateTime StartTime,
    string MeetingLink
    );
