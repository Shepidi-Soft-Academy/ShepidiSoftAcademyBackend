using MediatR;

namespace ShepidiSoft.Application.Features.Activities.Queries.GetActivityList;

public sealed record GetActivityListQuery : IRequest<ServiceResult<List<GetActivityListQueryResponse>>>;