using MediatR;

namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsList;

public sealed record GetNewsListQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<ServiceResult<List<GetNewsListQueryResponse>>>;
