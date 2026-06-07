using MediatR;

namespace ShepidiSoft.Application.Features.DocumentTopics.Command.CreateDocumentTopic;

public sealed record CreateDocumentTopicCommand(string Name) : 
    IRequest<ServiceResult<CreateDocumentTopicCommandResponse>>;
