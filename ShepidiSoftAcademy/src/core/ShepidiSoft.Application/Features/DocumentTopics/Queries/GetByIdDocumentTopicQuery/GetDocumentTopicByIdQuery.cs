using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.DocumentTopics.Queries.GetByIdDocumentTopicQuery
{
    public sealed record GetDocumentTopicByIdQuery(int Id)
        : IRequest<ServiceResult<DocumentTopicResponse>>;
}
