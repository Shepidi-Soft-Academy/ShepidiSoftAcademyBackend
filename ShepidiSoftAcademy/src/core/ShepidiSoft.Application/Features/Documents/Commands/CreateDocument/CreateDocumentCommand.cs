using MediatR;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed record CreateDocumentCommand(
int DocumentTopicId,
string Title,
string? Description,
string FileUrl) :  IRequest<ServiceResult<int>>;
