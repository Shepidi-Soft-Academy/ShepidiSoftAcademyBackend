using MediatR;
using Microsoft.AspNetCore.Http;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;

public sealed record CreateDocumentCommand(
    string Title,
    string? Description,
    int DocumentTopicId,
    IFormFile File              // FileUrl → IFormFile
) : IRequest<ServiceResult<CreateDocumentCommandResponse>>;