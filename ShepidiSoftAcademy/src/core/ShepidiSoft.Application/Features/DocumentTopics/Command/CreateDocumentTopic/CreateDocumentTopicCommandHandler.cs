using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.DocumentTopics.Command.CreateDocumentTopic;

public sealed class CreateDocumentTopicCommandHandler(
    IDocumentTopicRepository topicRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDocumentTopicCommand, ServiceResult<CreateDocumentTopicCommandResponse>>
{
    public async Task<ServiceResult<CreateDocumentTopicCommandResponse>> Handle(CreateDocumentTopicCommand request, CancellationToken cancellationToken)
    {
        // 1. Mükerrer isim kontrolü 
        var exists = await topicRepository.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());
        if (exists)
            return ServiceResult<CreateDocumentTopicCommandResponse>.Fail("Bu konu başlığı zaten mevcut.");

        var topic = new DocumentTopic { Name = request.Name };

       
        await topicRepository.AddAsync(topic);

        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateDocumentTopicCommandResponse>.Success(new CreateDocumentTopicCommandResponse(topic.Id));
    }
}