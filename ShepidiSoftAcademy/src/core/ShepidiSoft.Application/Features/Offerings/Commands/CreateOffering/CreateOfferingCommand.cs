using MediatR;

namespace ShepidiSoft.Application.Features.Offerings.Commands.CreateOffering;

public sealed record CreateOfferingCommand(
    string Title,
    string Description,
    bool IsActive

    ) : IRequest<ServiceResult<CreateOfferingResponse>>;


