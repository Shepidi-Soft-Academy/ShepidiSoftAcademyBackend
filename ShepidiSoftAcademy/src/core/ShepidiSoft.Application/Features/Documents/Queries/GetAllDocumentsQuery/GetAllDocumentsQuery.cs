using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetAllDocumentsQuery
{
    public sealed record GetAllDocumentsQuery() : IRequest<ServiceResult<List<GetDocumentQueryResponse>>>;
}
