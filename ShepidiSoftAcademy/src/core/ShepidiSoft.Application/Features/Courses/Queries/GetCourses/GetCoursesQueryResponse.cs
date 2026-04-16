namespace ShepidiSoft.Application.Features.Courses.Queries.GetCourses;

public sealed record GetCoursesQueryResponse(
int Id,
string Title,
bool IsOnline,
decimal? Price,
string Status,
string InstructorFullName,
string CoverImageUrl,
string ApplicationFormUrl
);
