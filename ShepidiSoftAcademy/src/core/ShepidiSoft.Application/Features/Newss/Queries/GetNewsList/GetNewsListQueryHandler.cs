using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsList;

public sealed class GetNewsListQueryHandler(
    INewsRepository newsRepository,
    IMapper mapper) : IRequestHandler<GetNewsListQuery, ServiceResult<List<GetNewsListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetNewsListQueryResponse>>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
    {
        var pagedNews = await newsRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);

        var response = mapper.Map<List<GetNewsListQueryResponse>>(pagedNews);

        return ServiceResult<List<GetNewsListQueryResponse>>.Success(response);
    }
}
