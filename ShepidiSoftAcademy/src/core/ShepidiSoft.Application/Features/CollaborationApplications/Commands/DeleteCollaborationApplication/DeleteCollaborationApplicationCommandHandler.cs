using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.DeleteCollaborationApplication;

public sealed class DeleteCollaborationApplicationCommandHandler(
    ICollaborationApplicationRepository collaborationApplicationRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCollaborationApplicationCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteCollaborationApplicationCommand request, CancellationToken cancellationToken)
    {
        var collaborationApplication = await collaborationApplicationRepository.GetByIdAsync(request.Id);

        if (collaborationApplication is null)
            return ServiceResult.Fail("Başvuru bulunamadı!", System.Net.HttpStatusCode.NotFound);

        collaborationApplicationRepository.Delete(collaborationApplication);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);

    }
}
