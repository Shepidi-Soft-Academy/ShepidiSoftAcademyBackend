using MediatR;

namespace ShepidiSoft.Application.Features.DocumentTopics.Commands.UpdateDocumentTopic;

public sealed record UpdateDocumentTopicCommand(
    int Id,
    string Name,
    string? Description) : IRequest<ServiceResult<Unit>>;