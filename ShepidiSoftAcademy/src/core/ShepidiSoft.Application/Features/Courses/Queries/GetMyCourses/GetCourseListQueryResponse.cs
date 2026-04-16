namespace ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses;

public sealed record GetCourseListQueryResponse(
 int Id,
 string Title,
 string Instructor,
 bool IsOnline,
 decimal? Price,
 string Status,
int InstructorId,
string  InstructorFullName,
 string? InstructorPhotoUrl
 );
