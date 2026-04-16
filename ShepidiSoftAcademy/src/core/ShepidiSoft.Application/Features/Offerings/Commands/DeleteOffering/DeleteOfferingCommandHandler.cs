using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Offerings.Commands.DeleteOffering;

public sealed class DeleteOfferingCommandHandler(
    IOfferingRepository offeringRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteOfferingCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        DeleteOfferingCommand request,
        CancellationToken cancellationToken)
    {
        var offering = await GetOfferingAsync(request.Id);

        if (offering is null)
            return ServiceResult.Fail("Hizmet bulunamadı!", HttpStatusCode.NotFound);

        await DeleteOfferingAsync(offering, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Offering?> GetOfferingAsync(int id)
        => await offeringRepository.GetByIdAsync(id);

    private async Task DeleteOfferingAsync(Offering offering, CancellationToken cancellationToken)
    {
        offeringRepository.Delete(offering);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}