using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Meetings.Commands.UpdateMeeting;

public sealed class UpdateMeetingCommandHandler(
    IMeetingRepository meetingRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(request.Id);

        if (meeting is null) {
            return ServiceResult.Fail("Toplantı bulunamadı!", HttpStatusCode.NotFound);
        }

        // Update the meeting properties
        meeting.Title = request.Title;
        meeting.Description = request.Description;
        meeting.StartTime = request.StartTime;
        meeting.MeetingLink = request.MeetingLink;

        meetingRepository.Update(meeting);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
