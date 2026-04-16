using MediatR;

namespace ShepidiSoft.Application.Features.Courses.Commands.CreateCourse;

public sealed record CreateCourseCommand(
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
    string? ApplicationFormUrl,
    decimal? Price) : IRequest<ServiceResult<CreateCourseCommandResponse>>;
