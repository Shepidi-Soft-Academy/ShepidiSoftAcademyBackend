using MediatR;

namespace ShepidiSoft.Application.Features.Documents.Commands.DeleteDocument;

public sealed record DeleteDocumentCommand(int Id) : IRequest<ServiceResult>;
