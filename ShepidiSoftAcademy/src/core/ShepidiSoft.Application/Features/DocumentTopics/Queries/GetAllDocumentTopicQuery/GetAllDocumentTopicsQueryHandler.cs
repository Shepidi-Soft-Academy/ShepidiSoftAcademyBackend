using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.DocumentTopics.Queries.GetAllDocumentTopicQuery;

namespace ShepidiSoft.Application.Features.DocumentTopics.Queries.GetAll;

public sealed class GetAllDocumentTopicsQueryHandler(
    IDocumentTopicRepository topicRepository) : IRequestHandler<GetAllDocumentTopicsQuery, ServiceResult<List<DocumentTopicResponse>>>
{
    public async Task<ServiceResult<List<DocumentTopicResponse>>> Handle(GetAllDocumentTopicsQuery request, CancellationToken cancellationToken)
    {
        var response = await topicRepository.WhereSelectAsync(
            predicate: x => true, 
            selector: x => new DocumentTopicResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Created),
            cancellationToken: cancellationToken
        );

        return ServiceResult<List<DocumentTopicResponse>>.Success(response);
    }
}