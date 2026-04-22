using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;

public sealed class CreateMeetingCommandHandler(
    IMeetingRepository meetingRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateMeetingCommand, ServiceResult<CreateMeetingCommandResponse>>
{
    public async Task<ServiceResult<CreateMeetingCommandResponse>> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting= mapper.Map<Meeting>(request); // use AutoMapper instead of manually mapping properties
        await meetingRepository.AddAsync(meeting);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ServiceResult<CreateMeetingCommandResponse>.Success(new CreateMeetingCommandResponse(meeting.Id));
    }
}
