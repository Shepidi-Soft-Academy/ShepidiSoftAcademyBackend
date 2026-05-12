using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.DocumentTopics.Command.DeleteDocumentTopic
{
    public sealed class DeleteDocumentTopicCommandHandler(
    IDocumentTopicRepository documentTopicRepository,
    IDocumentRepository documentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDocumentTopicCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(DeleteDocumentTopicCommand request, CancellationToken cancellationToken)
        {
            //  Konu var mı 
            var topic = await documentTopicRepository.GetByIdAsync(request.Id);

            if (topic == null)
                return ServiceResult<Unit>.Fail("Silmek istediğiniz konu başlığı bulunamadı.");

            //  İlişki Kontrolü: Bu konuya bağlı herhangi bir doküman var mı
           
            var hasLinkedDocuments = await documentRepository.AnyAsync(x => x.DocumentTopicId == request.Id);

            if (hasLinkedDocuments)
                return ServiceResult<Unit>.Fail("Bu konu başlığına ait yüklü dokümanlar olduğu için silemezsiniz" +
                    ". Önce dokümanları silmeli veya taşımalısınız.");

            //  Silme İşlemi
            documentTopicRepository.Delete(topic);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResult<Unit>.Success(Unit.Value);
        }
    }
}
