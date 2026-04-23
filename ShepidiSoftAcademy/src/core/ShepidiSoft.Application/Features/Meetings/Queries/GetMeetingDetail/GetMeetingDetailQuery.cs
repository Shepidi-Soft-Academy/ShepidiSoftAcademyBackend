using MediatR;

namespace ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingDetail;

public sealed record GetMeetingDetailQuery(int Id) : IRequest<ServiceResult<GetMeetingDetailQueryResponse>>;
