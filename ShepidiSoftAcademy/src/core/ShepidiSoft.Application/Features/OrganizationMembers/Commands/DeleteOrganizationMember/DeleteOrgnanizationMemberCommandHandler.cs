using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Commands.DeleteOrganizationMember;

public sealed class DeleteOrganizationMemberCommandHandler(
    IOrganizationMemberRepository organizationMemberRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteOrganizationMemberCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteOrganizationMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await organizationMemberRepository.GetByIdAsync(request.Id);

        if(member is null)
            ServiceResult.Fail("Üye bulunamadı",HttpStatusCode.NotFound);

        organizationMemberRepository.Delete(member!);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
