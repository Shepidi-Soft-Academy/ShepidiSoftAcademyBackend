using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument
{
    public sealed record CreateDocumentCommand(
    int DocumentTopicId,
    string Title,
    string? Description,
    string FileUrl) :  IRequest<ServiceResult<int>>;
}
