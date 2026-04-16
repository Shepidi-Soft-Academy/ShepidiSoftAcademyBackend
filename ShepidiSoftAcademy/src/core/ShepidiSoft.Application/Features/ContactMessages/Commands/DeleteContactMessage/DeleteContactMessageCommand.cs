using MediatR;

namespace ShepidiSoft.Application.Features.ContactMessages.Commands.DeleteContactMessage;

public sealed record DeleteContactMessageCommand(int Id) : IRequest<ServiceResult>;
