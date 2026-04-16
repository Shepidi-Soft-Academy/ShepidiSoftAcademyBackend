using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Offerings.Commands.UpdateOffering;

public sealed class UpdateOfferingCommandHandler(
    IOfferingRepository offeringRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateOfferingCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        UpdateOfferingCommand request,
        CancellationToken cancellationToken)
    {
        var offering = await GetOfferingAsync(request.Id);

        if (offering is null)
            return ServiceResult.Fail("Hizmet bulunamadı", HttpStatusCode.NotFound);

        ApplyUpdates(offering, request);
        await PersistAsync(offering, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Offering?> GetOfferingAsync(int id)
        => await offeringRepository.GetByIdAsync(id);

    private static void ApplyUpdates(
        Offering offering,
        UpdateOfferingCommand request)
    {
        offering.Title = request.Title;
        offering.IsActive = request.IsActive;
        offering.Description = request.Description;
    }

    private async Task PersistAsync(Offering offering, CancellationToken token)
    {
        offeringRepository.Update(offering);
        await unitOfWork.SaveChangesAsync(token);
    }
}