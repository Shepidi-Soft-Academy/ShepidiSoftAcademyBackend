namespace ShepidiSoft.Application.Features.ContactMessages.Queries.GetContactMessagesList;

public sealed record GetContactMessagesListQueryResponse(
    int Id,
    string Name,
    string Email,
    string Phone,
    string Content,
    DateTime SentAt,
    bool IsRead
    );

