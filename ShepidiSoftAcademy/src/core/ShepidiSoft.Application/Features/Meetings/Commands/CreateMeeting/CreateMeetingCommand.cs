using MediatR;

namespace ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;

public sealed record CreateMeetingCommand(
    string Title,
    string Description,
    string MeetingLink,
    DateTime StartTime
    ) :IRequest<ServiceResult<CreateMeetingCommandResponse>>;


