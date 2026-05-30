using MediatR;

namespace ShepidiSoft.Application.Features.Documents.Commands.UpdateDocument;

public sealed record UpdateDocumentCommand(
int Id,
int DocumentTopicId,
string Title,
string? Description,
string FileUrl
) : IRequest<ServiceResult>;
