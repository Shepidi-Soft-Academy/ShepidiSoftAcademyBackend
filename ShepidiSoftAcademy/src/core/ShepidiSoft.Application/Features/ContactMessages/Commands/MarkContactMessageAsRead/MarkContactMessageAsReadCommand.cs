using MediatR;

namespace ShepidiSoft.Application.Features.ContactMessages.Commands.MarkContactMessageAsRead;

public sealed record MarkContactMessageAsReadCommand(int Id) : IRequest<ServiceResult>;
