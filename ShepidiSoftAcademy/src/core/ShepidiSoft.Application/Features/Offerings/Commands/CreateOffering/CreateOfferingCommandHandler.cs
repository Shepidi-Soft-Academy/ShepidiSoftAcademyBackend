using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Offerings.Commands.CreateOffering;

public sealed class CreateOfferingCommandHandler(
    IOfferingRepository offeringRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    )
    : IRequestHandler<CreateOfferingCommand, ServiceResult<CreateOfferingResponse>>
{
    public async Task<ServiceResult<CreateOfferingResponse>> Handle(CreateOfferingCommand request, CancellationToken cancellationToken)
    {
        var offering= mapper.Map<Offering>(request);
        await PersistAsync(offering, cancellationToken);

        return ServiceResult<CreateOfferingResponse>
            .Success(new CreateOfferingResponse(offering.Id));

    }
    private async Task PersistAsync(Offering offering, CancellationToken cancellationToken)
    {
        await offeringRepository.AddAsync(offering); 
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }


}



