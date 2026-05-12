using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetDocumentsByStatusQuery
{
    public sealed class GetDocumentsByStatusQueryHandler(
     IDocumentRepository documentRepository) : IRequestHandler<GetDocumentsByStatusQuery, ServiceResult<List<GetDocumentQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetDocumentQueryResponse>>> Handle(GetDocumentsByStatusQuery request, CancellationToken cancellationToken)
        {
            // WhereSelectAsync kullanarak sadece belirli statüdeki dökümanları getiriyoruz
            var response = await documentRepository.WhereSelectAsync(
                predicate: x => x.Status == request.Status,
                selector: x => new GetDocumentQueryResponse(
                    x.Id,
                    x.Title,
                    x.FileUrl,
                    x.DocumentTopic.Name, // Kategori adı JOIN ile otomatik çekilir
                    x.Status,
                    x.PublishedAt
                ),
                cancellationToken: cancellationToken
            );

            return ServiceResult<List<GetDocumentQueryResponse>>.Success(response);
        }
    }
}
