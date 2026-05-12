using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus
{
    public class ChangeDocumentStatusCommandHandler(
     IDocumentRepository documentRepository,
     IUnitOfWork unitOfWork) : IRequestHandler<ChangeDocumentStatusCommand, ServiceResult<bool>>
    {
        public async Task<ServiceResult<bool>> Handle(ChangeDocumentStatusCommand request, CancellationToken cancellationToken)
        {
            var document = await documentRepository.GetByIdAsync(request.Id);
            if (document == null) return ServiceResult<bool>.Fail("Doküman bulunamadı.");

            //  dökümanın statüsü Admin günceller
            document.Status = request.NewStatus;
            document.Updated = DateTime.UtcNow;

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return ServiceResult<bool>.Success(true);
        }
    }
}
