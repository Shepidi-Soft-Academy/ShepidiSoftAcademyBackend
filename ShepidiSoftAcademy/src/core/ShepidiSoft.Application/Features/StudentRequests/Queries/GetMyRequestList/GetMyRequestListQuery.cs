using MediatR;

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetMyRequestList;

public sealed record GetMyRequestListQuery:IRequest<ServiceResult<List<GetMyRequestListQueryResponse>>>;
