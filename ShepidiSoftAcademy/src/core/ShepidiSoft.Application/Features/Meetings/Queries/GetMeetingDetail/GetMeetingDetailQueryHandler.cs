using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingDetail;

public sealed class GetMeetingDetailQueryHandler(
    IMeetingRepository meetingRepository,
    IUserService userService
    ) : IRequestHandler<GetMeetingDetailQuery, ServiceResult<GetMeetingDetailQueryResponse>>
{
    public async Task<ServiceResult<GetMeetingDetailQueryResponse>> Handle(GetMeetingDetailQuery request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(request.Id);

        if (meeting is null)
        {
            return ServiceResult<GetMeetingDetailQueryResponse>.Fail("Toplantı Bulunamadı", HttpStatusCode.NotFound);
        }

        string createdByName = string.Empty;

        if (meeting.CreatedBy.HasValue)
        {
            var userResult = await userService.GetByIdAsync(meeting.CreatedBy.Value, cancellationToken);
            if (userResult.IsSuccess && userResult.Data is not null)
            {
                createdByName = $"{userResult.Data.FirstName} {userResult.Data.LastName}";
            }
        }

        var response = new GetMeetingDetailQueryResponse(
            Title: meeting.Title,
            Description: meeting.Description,
            MeetingLink: meeting.MeetingLink,
            StartTime: meeting.StartTime,
            CreatedByName: createdByName
        );

        return ServiceResult<GetMeetingDetailQueryResponse>.Success(response);
    }
}
