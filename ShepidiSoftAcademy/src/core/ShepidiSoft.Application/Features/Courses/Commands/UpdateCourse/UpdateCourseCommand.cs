using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Commands.UpdateCourse;

public sealed record UpdateCourseCommand(
    int Id,
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
    ) : IRequest<ServiceResult>;
