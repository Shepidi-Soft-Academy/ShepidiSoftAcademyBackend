using MediatR;

namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingList;

public sealed class GetMeetingListQuery() : IRequest<ServiceResult<List<GetMeetingListQueryResponse>>>;
