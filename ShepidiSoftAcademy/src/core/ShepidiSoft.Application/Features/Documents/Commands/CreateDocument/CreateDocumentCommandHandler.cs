using AutoMapper;
using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;
using ShepidiSoft.Domain.Entities;

public sealed class CreateDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IMapper mapper,
    IDocumentTopicRepository documentTopicRepository,
    ICurrentUserService currentUserService, // Kullanıcı servisi eklendi
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDocumentCommand, ServiceResult<int>>
{
    public async Task<ServiceResult<int>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        try {
            // Konu başlığı kontrolü
            var topicExists = await documentTopicRepository.AnyAsync(x => x.Id == request.DocumentTopicId);
            if (!topicExists)
                return ServiceResult<int>.Fail("Seçilen konu başlığı sistemde bulunamadı.");


            var document = mapper.Map<Document>(request);


            var currentUserIdGuid = currentUserService.UserId;

            if (currentUserIdGuid == null)
                return ServiceResult<int>.Fail("Kullanıcı oturumu bulunamadı.");


            document.UploadedByUserId = currentUserIdGuid.Value.ToString();


            await documentRepository.AddAsync(document);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResult<int>.Success(document.Id);
        }
        catch (Exception ex)
        {
            // Gerçek hatayı görmek için InnerException'ı da dahil et
            var fullError = ex.InnerException != null ? ex.Message + " | " + ex.InnerException.Message : ex.Message;
            return ServiceResult<int>.Fail(fullError);
        }
    }
}