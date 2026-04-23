using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingList;

public sealed class GetMeetingListQueryHandler(
    IMeetingRepository meetingRepository,
    IMapper mapper
    ) : IRequestHandler<GetMeetingListQuery, ServiceResult<List<GetMeetingListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetMeetingListQueryResponse>>> Handle(GetMeetingListQuery request, CancellationToken cancellationToken)
    {
        var meetings = await meetingRepository.GetAllAsync();

        var response = mapper.Map<List<GetMeetingListQueryResponse>>(meetings);

        return ServiceResult<List<GetMeetingListQueryResponse>>.Success(response);
    }
}