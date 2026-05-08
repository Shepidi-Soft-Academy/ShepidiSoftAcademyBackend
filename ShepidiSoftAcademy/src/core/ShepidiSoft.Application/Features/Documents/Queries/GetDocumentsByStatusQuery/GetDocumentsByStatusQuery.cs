using MediatR;
using ShepidiSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetDocumentsByStatusQuery
{
    public sealed record GetDocumentsByStatusQuery(DocumentStatus Status) 
        : IRequest<ServiceResult<List<GetDocumentQueryResponse>>>;
}
