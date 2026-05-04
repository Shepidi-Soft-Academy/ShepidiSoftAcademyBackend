using MediatR;

namespace ShepidiSoft.Application.Features.Newss.Commands.DeleteNewss;

public sealed record DeleteNewsCommand(int Id) : IRequest<ServiceResult>;
