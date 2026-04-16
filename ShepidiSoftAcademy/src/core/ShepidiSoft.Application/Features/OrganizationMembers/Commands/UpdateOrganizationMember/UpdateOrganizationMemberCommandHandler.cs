using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities.Organizations;
using System.Net;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Commands.UpdateOrganizationMember;

public sealed class UpdateOrganizationMemberCommandHandler(
    IOrganizationMemberRepository organizationMemberRepository,
    IOrganizationPositionRepository organizationPositionRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateOrganizationMemberCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        UpdateOrganizationMemberCommand request,
        CancellationToken cancellationToken)
    {
      
        var member = await organizationMemberRepository
            .GetByIdWithPositionsAsync(request.Id, cancellationToken);

        if (member is null)
            return ServiceResult.Fail(
                $"Member '{request.Id}' bulunamadı.",
                HttpStatusCode.NotFound);

     
        var validPositionCount = await organizationPositionRepository
            .CountAsync(x => request.PositionIds.Contains(x.Id) && x.IsActive);

        if (validPositionCount != request.PositionIds.Count)
            return ServiceResult.Fail(
                "Bazı pozisyonlar bulunamadı veya aktif değil.",
                HttpStatusCode.NotFound);

       
        member.Positions.Clear();
        foreach (var positionId in request.PositionIds)
        {
            member.Positions.Add(new OrganizationMemberPosition
            {
                OrganizationPositionId = positionId
            });
        }

        organizationMemberRepository.Update(member);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}