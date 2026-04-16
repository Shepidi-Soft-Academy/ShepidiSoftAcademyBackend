using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;
using ShepidiSoft.Domain.Entities;
using System.Net;

public sealed class DeleteActivityCommandHandler(
    IActivityRepository activityRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteActivityCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        DeleteActivityCommand request,
        CancellationToken cancellationToken)
    {
        var activity = await GetActivityAsync(request.Id);

        if (activity is null)
            return ServiceResult.Fail("Aktivite bulunamadı", HttpStatusCode.NotFound);

        await DeleteActivityAsync(activity, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Activity?> GetActivityAsync(int id)
        => await activityRepository.GetByIdAsync(id);

    private async Task DeleteActivityAsync(Activity activity, CancellationToken cancellationToken)
    {
        activityRepository.Delete(activity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}