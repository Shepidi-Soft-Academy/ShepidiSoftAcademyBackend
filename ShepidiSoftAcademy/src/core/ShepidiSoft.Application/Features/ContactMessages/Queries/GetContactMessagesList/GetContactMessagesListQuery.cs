using MediatR;

namespace ShepidiSoft.Application.Features.ContactMessages.Queries.GetContactMessagesList;

public sealed record GetContactMessagesListQuery() : IRequest<ServiceResult<List<GetContactMessagesListQueryResponse>>>;
