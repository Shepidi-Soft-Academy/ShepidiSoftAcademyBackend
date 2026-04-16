using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CareerApplications.Commands.UpdateCareerApplicationStatus;

public sealed class UpdateCareerApplicationStatusCommandHandler(
    ICareerApplicationRepository careerApplicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCareerApplicationStatusCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateCareerApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        var careerApplication = await careerApplicationRepository.GetByIdAsync(request.Id);

        if (careerApplication is null)
            return ServiceResult.Fail("Başvuru bulunamadı.", HttpStatusCode.NotFound);

        careerApplication.Status = request.Status;
        careerApplication.AdminNote = request.AdminResponse;

        careerApplicationRepository.Update(careerApplication);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);

        
    }
}
