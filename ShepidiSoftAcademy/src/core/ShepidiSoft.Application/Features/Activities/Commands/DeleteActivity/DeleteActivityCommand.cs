using MediatR;

namespace ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;

public sealed record DeleteActivityCommand(int Id) : IRequest<ServiceResult>;
