using MediatR;
using ShepidiSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus
{
    public sealed record ChangeDocumentStatusCommand(int Id, DocumentStatus NewStatus) : IRequest<ServiceResult<bool>>;
}
