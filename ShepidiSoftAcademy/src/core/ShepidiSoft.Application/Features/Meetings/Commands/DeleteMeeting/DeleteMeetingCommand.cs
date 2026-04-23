using MediatR;

namespace ShepidiSoft.Application.Features.Meetings.Commands.DeleteMeeting;

public sealed record DeleteMeetingCommand(int Id) : IRequest<ServiceResult>;
