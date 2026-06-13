using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;


namespace ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus;

public class ChangeDocumentStatusCommandHandler(
 IDocumentRepository documentRepository,
 IUnitOfWork unitOfWork) : IRequestHandler<ChangeDocumentStatusCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ChangeDocumentStatusCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetByIdAsync(request.Id);

        if (document is null)
            return ServiceResult.Fail("Döküman bulunamadı.");

        document.Status = request.NewStatus;
        document.Updated = DateTime.UtcNow;

         documentRepository.Update(document);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}
