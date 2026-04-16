using MediatR;

namespace ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;

public sealed record GetActivityDetailQuery(int Id)
    : IRequest<ServiceResult<GetActivityDetailQueryResponse>>;
