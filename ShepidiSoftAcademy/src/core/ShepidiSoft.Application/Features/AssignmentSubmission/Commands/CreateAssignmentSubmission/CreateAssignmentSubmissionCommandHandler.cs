using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.AssignmentSubmission.Commands.CreateAssignmentSubmission;

public sealed class CreateAssignmentSubmissionCommandHandler(
    ICurrentUserService currentUserService,
    IAssignmentSubmissionRepository assignmentSubmissionRepository,
    IAssignmentRepository assignmentRepository
    ) : IRequestHandler<CreateAssignmentSubmissionCommand, ServiceResult<int>>
{
    public Task<ServiceResult<int>> Handle(CreateAssignmentSubmissionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
