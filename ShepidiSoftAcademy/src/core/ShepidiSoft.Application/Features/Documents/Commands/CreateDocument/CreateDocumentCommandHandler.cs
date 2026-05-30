using MediatR;
using ShepidiSoft.Application.Contracts;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed class CreateDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IFileStorageService fileStorageService,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateDocumentCommand, ServiceResult<CreateDocumentCommandResponse>>
{
    public async Task<ServiceResult<CreateDocumentCommandResponse>> Handle(
        CreateDocumentCommand request,
        CancellationToken cancellationToken)
    {
        string fileUrl;
        try
        {
            fileUrl = await fileStorageService.SaveAsync(
                request.File,
                folder: "documents",
                cancellationToken);
        }
        catch (ArgumentException ex)
        {
            return ServiceResult<CreateDocumentCommandResponse>.Fail(ex.Message);
        }

        // 2. Entity oluştur
        var document = new Document
        {
            Title = request.Title,
            Description = request.Description,
            DocumentTopicId = request.DocumentTopicId,
            FileUrl = fileUrl,
        };

        await documentRepository.AddAsync(document);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateDocumentCommandResponse>.Success(
            new CreateDocumentCommandResponse(document.Id));
    }
}