using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Activities.Commands.UpdateActivity;

public sealed class UpdateActivityCommandHandler(
    IActivityRepository activityRepository,
    IUnitOfWork unitOfWork   
    ) : IRequestHandler<UpdateActivityCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await GetActivityAsync( request.Id );


        if (activity is null)
            return ServiceResult.Fail("Aktivite bulunamadı", HttpStatusCode.NotFound);

        ApplyUpdates(activity, request);
        await PersistAsync(activity, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Activity?> GetActivityAsync(int id)
      => await activityRepository.GetByIdAsync(id);

    private static void ApplyUpdates(
       Activity activity,
       UpdateActivityCommand request)
    {
       activity.Title=request.Title;
       activity.Location=request.Location;
       activity.Description=request.Description;
       activity.Date=request.Date;
       activity.IsOnline=request.IsOnline;
       activity.OnlineMeetingUrl=request.OnlineMeetingUrl;
    }


    private async Task PersistAsync(Activity activity,CancellationToken token)
    {
        activityRepository.Update(activity);
        await unitOfWork.SaveChangesAsync(token);
    }


}
