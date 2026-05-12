using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.DocumentTopics.Queries.GetByIdDocumentTopicQuery;

namespace ShepidiSoft.Application.Features.DocumentTopics.Queries.GetById;

public sealed class GetDocumentTopicByIdQueryHandler(
    IDocumentTopicRepository topicRepository) : IRequestHandler<GetDocumentTopicByIdQuery, ServiceResult<DocumentTopicResponse>>
{
    public async Task<ServiceResult<DocumentTopicResponse>> Handle(GetDocumentTopicByIdQuery request, CancellationToken cancellationToken)
    {
        var topic = await topicRepository.GetByIdAsync(request.Id);

        if (topic == null)
            return ServiceResult<DocumentTopicResponse>.Fail("Konu başlığı bulunamadı.");

        var response = new DocumentTopicResponse(
            topic.Id,
            topic.Name,
            topic.Description,
            topic.Created);

        return ServiceResult<DocumentTopicResponse>.Success(response);
    }
}