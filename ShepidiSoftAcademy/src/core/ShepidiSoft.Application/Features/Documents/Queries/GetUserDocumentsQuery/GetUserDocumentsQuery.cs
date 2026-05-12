using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetUserDocumentsQuery
{
    public sealed record GetUserDocumentsQuery(string UserId)
        : IRequest<ServiceResult<List<GetDocumentQueryResponse>>>;
}
