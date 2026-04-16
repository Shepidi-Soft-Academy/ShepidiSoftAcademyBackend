using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CareerApplications.Commands.DeleteCareerApplication;

public sealed class DeleteCareerApplicationCommandHandler(
    ICareerApplicationRepository careerApplicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCareerApplicationCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCareerApplicationCommand request, CancellationToken cancellationToken)
    {
        var careerApplication = await careerApplicationRepository.GetByIdAsync(request.Id);

        if (careerApplication is null)
            return ServiceResult.Fail("Başvuru Bulunamadı",HttpStatusCode.NotFound);

        careerApplicationRepository.Delete(careerApplication);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
        
    }
}
