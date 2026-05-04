using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Newss.Commands.DeleteNewss;
using System.Net;

namespace ShepidiSoft.Application.Features.Newss.Commands.DeleteNews;

public sealed class DeleteNewsCommandHandler(
    INewsRepository newsRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteNewsCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        //Haberi repository üzerinden ID ile sorgula
        var news = await newsRepository.GetByIdAsync(request.Id);

        //Haber bulunamadıysa Fail dön
        if (news is null)
        {
            return ServiceResult.Fail("Haber bulunamadı!",HttpStatusCode.NotFound);
        }

        //Haberi silme işlemine sok
        newsRepository.Delete(news);

        //Değişiklikleri veritabanına yansıt
        await unitOfWork.SaveChangesAsync(cancellationToken);

        //Standartlara uygun olarak NoContent dön
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}