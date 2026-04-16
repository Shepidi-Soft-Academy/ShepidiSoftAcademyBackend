using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Activities.Commands.CreateActivity;

public sealed class CreateActivityCommandHandler(
    IActivityRepository activityRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    )
        : IRequestHandler<CreateActivityCommand, ServiceResult<CreateActivityCommandResponse>>
{
    public async Task<ServiceResult<CreateActivityCommandResponse>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = mapper.Map<Activity>(request);
        await PersistAsync(activity,cancellationToken);

        return ServiceResult<CreateActivityCommandResponse>
           .Success(new CreateActivityCommandResponse(activity.Id));
    }

    //Db'ye yansıt
    private async Task PersistAsync(Activity activity, CancellationToken cancellationToken)
    {
        await activityRepository.AddAsync(activity);  // Memory düzeyinde kayıt eder
        await unitOfWork.SaveChangesAsync( cancellationToken);
    }
}

   