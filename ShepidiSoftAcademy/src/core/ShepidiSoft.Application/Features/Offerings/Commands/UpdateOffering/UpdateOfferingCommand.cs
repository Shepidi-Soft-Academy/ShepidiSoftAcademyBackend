using MediatR;

namespace ShepidiSoft.Application.Features.Offerings.Commands.UpdateOffering;

public sealed record class UpdateOfferingCommand(
    int Id,
    string Title,
    string Description,
    bool IsActive
    ) : IRequest<ServiceResult>;
