using MediatR;

namespace ShepidiSoft.Application.Features.Offerings.Commands.DeleteOffering;

public sealed record DeleteOfferingCommand(int Id)
    : IRequest<ServiceResult>;
