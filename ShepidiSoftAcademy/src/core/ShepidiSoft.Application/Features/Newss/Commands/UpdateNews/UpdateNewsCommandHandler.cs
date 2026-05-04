using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Newss.Commands.UpdateNewss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Newss.Commands.UpdateNews
{
    public sealed class UpdateNewsCommandHandler(
        INewsRepository newsRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateNewsCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await newsRepository.GetByIdAsync(request.Id);

            if(news is null)
            {
                return ServiceResult.Fail("Güncellenmek istenen haber bulunamadı");
            }

            mapper.Map(request, news);

            //Başlık değişmiş olabileceğinden dolayı slugu yeniden uretiyoruz
            news.Slug = request.Title.ToLower()
                .Replace("", "-")
                .Replace("?", "")
                .Replace("!", "");

            if(request.IsPublished && news.IsPublished == default)
            {
                news.PublishedAt = DateTime.Now;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
