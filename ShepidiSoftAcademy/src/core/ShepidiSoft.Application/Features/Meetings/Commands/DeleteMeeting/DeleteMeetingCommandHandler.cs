using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Meetings.Commands.DeleteMeeting;

public sealed class DeleteMeetingCommandHandler(
    IMeetingRepository meetingRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMeetingCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(request.Id);
        if (meeting is null)
        {
            return ServiceResult.Fail("Toplantı bulunamadı!");
        }

        meetingRepository.Delete(meeting);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}