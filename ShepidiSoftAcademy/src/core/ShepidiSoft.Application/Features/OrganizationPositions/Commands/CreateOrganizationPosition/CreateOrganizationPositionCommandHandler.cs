using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Commands.CreateOrganizationPosition;

public sealed class CreateOrganizationPositionCommandHandler(
    IOrganizationPositionRepository organizationPositionRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<CreateOrganizationPositionCommand, ServiceResult<CreateOrganizationPositionCommandResponse>>
{
    public async Task<ServiceResult<CreateOrganizationPositionCommandResponse>> Handle(CreateOrganizationPositionCommand request, CancellationToken cancellationToken)
    {
        var organizationPosition = mapper.Map<OrganizationPosition>(request);

       

        await organizationPositionRepository.AddAsync(organizationPosition);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateOrganizationPositionCommandResponse>
          .Success(new CreateOrganizationPositionCommandResponse(organizationPosition.Id));
    }
}
