using MediatR;

namespace ShepidiSoft.Application.Features.AssignmentSubmission.Commands.CreateAssignmentSubmission;

public sealed record CreateAssignmentSubmissionCommand(

    ) : IRequest<ServiceResult<int>>;
