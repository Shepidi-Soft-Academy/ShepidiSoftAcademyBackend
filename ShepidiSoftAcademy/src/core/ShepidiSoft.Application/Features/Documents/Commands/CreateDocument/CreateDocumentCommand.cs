using MediatR;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed record CreateDocumentCommand(
    string Title,
    string? Description,
    int DocumentTopicId,
    string FileUrl
    ): IRequest<ServiceResult<CreateDocumentCommandResponse>>;
