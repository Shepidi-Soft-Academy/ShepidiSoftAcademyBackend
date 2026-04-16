namespace ShepidiSoft.API;

public sealed record UpdateCourseRequest(
    string Title,
    string Description,
    string Location,
    int InstructorId,
    bool IsOnline,
    string MeetingUrl,
    string CoverImageUrl,
    int DurationInWeeks,
    DateTime StartedDate,
    DateTime EndDate,
    string Status,
    decimal? Price
    );
