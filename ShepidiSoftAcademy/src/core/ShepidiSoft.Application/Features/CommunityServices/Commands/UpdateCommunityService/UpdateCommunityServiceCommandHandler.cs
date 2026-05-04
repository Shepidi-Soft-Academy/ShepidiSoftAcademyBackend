using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.UpdateCommunityService;

public sealed class UpdateCommunityServiceCommandHandler(
    ICommunityServiceRepository communityServiceRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCommunityServiceCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateCommunityServiceCommand request, CancellationToken cancellationToken)
    {
        var communityService = await communityServiceRepository.GetByIdAsync(request.Id);

        if(communityService is null)
            return ServiceResult.Fail("Hizmet bulunamadı!",HttpStatusCode.NotFound);

        communityService.Title = request.Title;
        communityService.Description = request.Description;
        communityService.IsActive = request.IsActive;
        communityService.ImageUrl = request.ImageUrl;

        communityServiceRepository.Update(communityService);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);


    }
}
