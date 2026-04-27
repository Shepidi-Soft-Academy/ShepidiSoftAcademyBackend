using MediatR;

namespace ShepidiSoft.Application.Features.Meetings.Commands.UpdateMeeting;

public sealed record UpdateMeetingCommand(
    int Id,
    string Title,
    string Description,
    DateTime StartTime,
    string MeetingLink
    ):IRequest<ServiceResult>;
