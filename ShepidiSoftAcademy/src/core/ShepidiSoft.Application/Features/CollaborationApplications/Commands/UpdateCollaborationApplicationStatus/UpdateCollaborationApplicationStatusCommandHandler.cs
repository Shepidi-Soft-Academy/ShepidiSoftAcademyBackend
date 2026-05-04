using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.UpdateCollaborationApplicationStatus;

public sealed class UpdateCollaborationApplicationStatusCommandHandler(
    ICollaborationApplicationRepository collaborationApplicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCollaborationApplicationStatusCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateCollaborationApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        var collaboration = await collaborationApplicationRepository.GetByIdAsync(request.Id);

        if (collaboration is null)
            return ServiceResult.Fail("Başvuru bulunamadı!", HttpStatusCode.NotFound);

        collaboration.Status = request.Status;

        collaborationApplicationRepository.Update(collaboration);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);


    }
}
