using MediatR;
using ShepidiSoft.Application.Contracts;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Documents.Commands.DeleteDocument;

public sealed class DeleteDocumentCommandHandler(
 IDocumentRepository documentRepository,
 IFileStorageService fileStorageService,
 IUnitOfWork unitOfWork) : IRequestHandler<DeleteDocumentCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetByIdAsync(request.Id);

        if (document is null)
            return ServiceResult.Fail("Silinmek istenen doküman bulunamadı.", HttpStatusCode.NotFound);

        try
        {
            fileStorageService.Delete(document.FileUrl);
        }
        catch (Exception ex)
        {
            return ServiceResult.Fail($"Dosya silinemedi, işlem iptal edildi.", HttpStatusCode.InternalServerError);
        }

        documentRepository.Delete(document);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
