using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Commands.DeleteOrganizationPosition;

public sealed class DeleteOrganizationPositionCommandHandler(
    IOrganizationPositionRepository organizationPositionRepository,
    IUnitOfWork unitOfWork

    ) : IRequestHandler<DeleteOrganizationPositionCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteOrganizationPositionCommand request, CancellationToken cancellationToken)
    {
        var organizationPosition= await organizationPositionRepository.GetByIdAsync( request.Id );

        if (organizationPosition is null)
            return ServiceResult.Fail("Pozisyon Bulunamadı", HttpStatusCode.NotFound);
        
        organizationPositionRepository.Delete(organizationPosition);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}


