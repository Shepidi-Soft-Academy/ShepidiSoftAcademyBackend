using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.DocumentTopics.Command.DeleteDocumentTopic
{
    public sealed record DeleteDocumentTopicCommand(int Id) 
        : IRequest<ServiceResult<Unit>>;
}
