using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.DocumentTopics.Queries.GetAllDocumentTopicQuery
{
    public sealed record GetAllDocumentTopicsQuery() 
        : IRequest<ServiceResult<List<DocumentTopicResponse>>>;
}
