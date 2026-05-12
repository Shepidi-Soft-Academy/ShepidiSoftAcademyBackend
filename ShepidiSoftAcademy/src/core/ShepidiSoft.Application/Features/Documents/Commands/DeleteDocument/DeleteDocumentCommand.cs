using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.DeleteDocument
{
    public sealed record DeleteDocumentCommand(int Id) : IRequest<ServiceResult<bool>>;
}
