using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Queries.GetUserDocumentsQuery
{
    public sealed class GetUserDocumentsQueryHandler(
    IDocumentRepository documentRepository) : IRequestHandler<GetUserDocumentsQuery, ServiceResult<List<GetDocumentQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetDocumentQueryResponse>>> Handle(GetUserDocumentsQuery request, CancellationToken cancellationToken)
        {
            // Predicate kısmında dökümanın UploadedByUserId alanını kontrol ediyoruz
            var response = await documentRepository.WhereSelectAsync(
                predicate: x => x.UploadedByUserId == request.UserId,
                selector: x => new GetDocumentQueryResponse(
                    x.Id,
                    x.Title,
                    x.FileUrl,
                    x.DocumentTopic.Name,
                    x.Status,
                    x.PublishedAt
                ),
                cancellationToken: cancellationToken
            );

            // Sonuçları en yeni tarihe göre sıralamak
            var orderedResponse = response.OrderByDescending(x => x.PublishedAt).ToList();

            return ServiceResult<List<GetDocumentQueryResponse>>.Success(orderedResponse);
        }
    }
}
