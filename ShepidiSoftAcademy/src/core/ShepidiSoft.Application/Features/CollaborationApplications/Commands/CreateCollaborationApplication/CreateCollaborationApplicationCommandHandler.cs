using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;

public sealed class CreateCollaborationApplicationCommandHandler(
    ICollaborationApplicationRepository collaborationApplicationRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<CreateCollaborationApplicationCommand, ServiceResult<CreateCollaborationApplicationCommandResponse>>
{
    public async Task<ServiceResult<CreateCollaborationApplicationCommandResponse>> Handle(CreateCollaborationApplicationCommand request, CancellationToken cancellationToken)
    {
        var collaborationApplication = mapper.Map<CollaborationApplication>(request);

        await collaborationApplicationRepository.AddAsync(collaborationApplication);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateCollaborationApplicationCommandResponse>.Success(new CreateCollaborationApplicationCommandResponse(collaborationApplication.Id));
    }
}
