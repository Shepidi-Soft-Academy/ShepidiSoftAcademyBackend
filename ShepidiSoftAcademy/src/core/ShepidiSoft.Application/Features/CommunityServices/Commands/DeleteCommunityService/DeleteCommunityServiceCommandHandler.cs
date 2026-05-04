using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.DeleteCommunityService;

public sealed class DeleteCommunityServiceCommandHandler(
    ICommunityServiceRepository communityServiceRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCommunityServiceCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCommunityServiceCommand request, CancellationToken cancellationToken)
    {
        var communityService =  await communityServiceRepository.GetByIdAsync(request.Id);

        if (communityService is null)
            return ServiceResult.Fail("Hizmet bulunamadı!", HttpStatusCode.NotFound);

        communityServiceRepository.Delete(communityService);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ServiceResult.Success(HttpStatusCode.NoContent);

    }
}
