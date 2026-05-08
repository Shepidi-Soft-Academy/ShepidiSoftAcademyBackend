using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.UpdateDocument
{
    public sealed record UpdateDocumentCommand(
    int Id,
    int DocumentTopicId,
    string Title,
    string? Description,
    string FileUrl
) : IRequest<ServiceResult<int>>;
}
