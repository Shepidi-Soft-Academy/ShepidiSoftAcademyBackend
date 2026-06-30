using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Organizations.Commands.UpdateOrganization;

public sealed class UpdateOrganizationCommandHandler(
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper 
) : IRequestHandler<UpdateOrganizationCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        UpdateOrganizationCommand request,
        CancellationToken cancellationToken)
    {
       
        var organization = await organizationRepository.GetByIdAsync(request.Id);

        if (organization is null)
        {
            return ServiceResult.Fail(
                "Organizasyon bulunamadı.",
                HttpStatusCode.NotFound);
        }


        mapper.Map(request, organization);

       
        organizationRepository.Update(organization);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}