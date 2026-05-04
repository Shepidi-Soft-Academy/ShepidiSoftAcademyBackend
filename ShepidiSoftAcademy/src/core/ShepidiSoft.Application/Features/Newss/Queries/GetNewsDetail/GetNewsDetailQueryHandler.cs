using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;


namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsDetail;

public sealed class GetNewsDetailQueryHandler(
    INewsRepository newsRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
) : IRequestHandler<GetNewsDetailQuery, ServiceResult<GetNewsDetailQueryResponse>>
{
    public async Task<ServiceResult<GetNewsDetailQueryResponse>> Handle(GetNewsDetailQuery request, CancellationToken cancellationToken)
    {

        var news = await newsRepository.GetBySlugAsync(request.Slug);

        if (news is null)
        {
            return ServiceResult<GetNewsDetailQueryResponse>.Fail("Haber bulunamadı!");
        }

        
        news.ViewCount++;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<GetNewsDetailQueryResponse>(news);

        return ServiceResult<GetNewsDetailQueryResponse>.Success(response);
    }
}