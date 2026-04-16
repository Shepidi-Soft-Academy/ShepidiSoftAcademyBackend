using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.ContactMessages.Queries.GetContactMessagesList;

public sealed class GetContactMessagesListQueryHandler(
    IContactMessageRepository contactMessageRepository,
    IMapper mapper
    ) : IRequestHandler<GetContactMessagesListQuery, ServiceResult<List<GetContactMessagesListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetContactMessagesListQueryResponse>>> Handle(GetContactMessagesListQuery request, CancellationToken cancellationToken)
    {
        var contactMessages = await contactMessageRepository.GetAllAsync();

        var response = mapper.Map<List<GetContactMessagesListQueryResponse>>(contactMessages);

        return ServiceResult<List<GetContactMessagesListQueryResponse>>.Success(response);
    }
}
