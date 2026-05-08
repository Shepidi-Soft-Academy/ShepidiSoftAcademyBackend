using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.DeleteDocument
{
    public sealed class DeleteDocumentCommandHandler(
     IDocumentRepository documentRepository,
     IUnitOfWork unitOfWork) : IRequestHandler<DeleteDocumentCommand, ServiceResult<bool>>
    {
        public async Task<ServiceResult<bool>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            //  Döküman var mı kontrolü
            var document = await documentRepository.GetByIdAsync(request.Id);

            if (document == null)
                return ServiceResult<bool>.Fail("Silinmek istenen doküman bulunamadı.");

          
            
            documentRepository.Delete(document);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResult<bool>.Success(true);
        }
    }
}
