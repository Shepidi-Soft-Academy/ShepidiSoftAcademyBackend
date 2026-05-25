using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.UpdateDocument
{


    public sealed class UpdateDocumentCommandHandler(
        IDocumentRepository documentRepository,
         IMapper mapper,
          ICurrentUserService currentUserService,
        IDocumentTopicRepository documentTopicRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateDocumentCommand, ServiceResult<int>>
    {
        public async Task<ServiceResult<int>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await documentRepository.GetByIdAsync(request.Id);
            if (document == null) return ServiceResult<int>.Fail("Doküman bulunamadı.",HttpStatusCode.NotFound);

            //  Sadece döküman sahibi günclesın 
             if (document.UploadedByUserId != currentUserService.UserId.ToString()) 
                return ServiceResult<int>.Fail("Sadece kendi dokümanınızı güncelleyebilirsiniz.");

           //Sadece onaylanmamış  döküman güncellenebilir
            if (document.Status != DocumentStatus.Bekliyor)
            {
                return ServiceResult<int>.Fail("Onaylanmış veya reddedilmiş bir doküman üzerinde değişiklik yapamazsınız.");
            }

            // AutoMapper 
            mapper.Map(request, document);
            document.Updated = DateTime.UtcNow;

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return ServiceResult<int>.Success(document.Id);
        }
    }
}
