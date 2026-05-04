using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;
using ShepidiSoft.Application.Features.Newss.Commands.CreateNewss;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Newss.Commands.CreateNews;

public sealed class CreateNewsCommandHandler(
    INewsRepository newsRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateNewsCommand, ServiceResult<CreateNewsCommandResponse>>
{
    public async Task<ServiceResult<CreateNewsCommandResponse>> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = mapper.Map<News>(request);

        //Basliktan url dostu slug uretmek icin
        news.Slug = request.Title.ToLower()
                    .Replace(" ", "-")
                    .Replace("?", "")
                    .Replace("!", "");

        //Yayınlanma tarihini kontrol etmek icin
        if (request.IsPublished)
        {
            news.PublishedAt = DateTime.Now;
        }

        await newsRepository.AddAsync(news);

        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            return ServiceResult<CreateNewsCommandResponse>.Fail("Haber kaydedilirken bir hata oluştu.");
        }

        var response = new CreateNewsCommandResponse(news.Id, news.Slug);
        return ServiceResult<CreateNewsCommandResponse>.Success(response);
    }
}
