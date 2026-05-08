using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ShepidiSoft.Domain.Entities;
using System.Text;
using System.Threading.Tasks;


namespace ShepidiSoft.Application.Features.Documents.Queries.GetAllDocumentsQuery
{
    public sealed class GetAllDocumentsQueryHandler(
    IDocumentRepository documentRepository,
    IMapper mapper) : IRequestHandler<GetAllDocumentsQuery, ServiceResult<List<GetDocumentQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetDocumentQueryResponse>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
        {
            var response = await documentRepository.WhereSelectAsync(
         predicate: x => true, // Filtre yok, tüm dökümanlar
         selector: x => new GetDocumentQueryResponse(
             x.Id,
             x.Title,
             x.FileUrl,
             x.DocumentTopic.Name, // Join otomatik yapılır
             x.Status,
             x.PublishedAt
         ),
         cancellationToken: cancellationToken
     );

            return ServiceResult<List<GetDocumentQueryResponse>>.Success(response);
        }
    }
}
