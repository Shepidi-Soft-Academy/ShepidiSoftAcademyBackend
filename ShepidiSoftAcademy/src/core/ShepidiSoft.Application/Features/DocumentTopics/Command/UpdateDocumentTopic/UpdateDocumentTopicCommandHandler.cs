using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.DocumentTopics.Commands.UpdateDocumentTopic;

public sealed class UpdateDocumentTopicCommandHandler(
    IDocumentTopicRepository documentTopicRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateDocumentTopicCommand, ServiceResult<Unit>>
{
    public async Task<ServiceResult<Unit>> Handle(UpdateDocumentTopicCommand request, CancellationToken cancellationToken)
    {
        // Mevcut kaydı getir
        var topic = await documentTopicRepository.GetByIdAsync(request.Id);
        if (topic == null)
            return ServiceResult<Unit>.Fail("Güncellenmek istenen konu başlığı bulunamadı.");

        //  İsim değişikliği varsa Unique kontrolü yap
        // Eğer isim değişmişse, yeni ismin veritabanında başkası tarafından kullanılıp kullanılmadığına kontrol 
        if (topic.Name.ToLower() != request.Name.ToLower())
        {
            var nameExists = await documentTopicRepository.AnyAsync(x =>
                x.Name.ToLower() == request.Name.ToLower() && x.Id != request.Id);

            if (nameExists)
                return ServiceResult<Unit>.Fail("Bu isimde başka bir konu başlığı zaten mevcut.");
        }

      
        mapper.Map(request, topic);

        
        documentTopicRepository.Update(topic);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<Unit>.Success(Unit.Value);
    }
}