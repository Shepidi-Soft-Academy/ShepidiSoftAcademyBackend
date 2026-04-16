namespace ShepidiSoft.Application.Features.Courses.Queries.GetCourseDetail;

public sealed record GetCourseDetailQueryResponse(
    string Title,
    string Description,
    string Location,
    string InstructorFullName,
    string InstructorPhotoUrl,
    bool IsOnline,
    string MeetingUrl,
    string CoverImageUrl,
    int DurationInWeeks,
    DateTime StartedDate,
    DateTime EndDate,
    string Status,
    decimal? Price,
    string ApplicationFormUrl
    );
