using MediatR;

namespace ShepidiSoft.Application.Features.ContactMessages.Commands.CreateContactMessage;

public sealed record CreateContactMessageCommand(
    string Name,
    string Email,
    string? Phone,
    string Content
    ) : IRequest<ServiceResult<int>>;

