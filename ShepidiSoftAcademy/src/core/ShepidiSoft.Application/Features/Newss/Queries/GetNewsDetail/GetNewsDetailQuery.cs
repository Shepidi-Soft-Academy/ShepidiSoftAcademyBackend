using MediatR;

namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsDetail;

public sealed record GetNewsDetailQuery(string Slug) : IRequest<ServiceResult<GetNewsDetailQueryResponse>>;
