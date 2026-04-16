using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Assignments.Commands.DeleteAssignment;
using ShepidiSoft.Domain.Entities;
using System.Net;

public sealed class DeleteAssignmentCommandHandler(
    IAssignmentRepository assignmentRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteAssignmentCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        DeleteAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var assignment = await GetAssignmentAsync(request.Id);

        if (assignment is null)
            return ServiceResult.Fail("Ödev bulunamadı", HttpStatusCode.NotFound);

        await DeleteAssignmentAsync(assignment, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Assignment?> GetAssignmentAsync(int id)
        => await assignmentRepository.GetByIdAsync(id);

    private async Task DeleteAssignmentAsync(Assignment assignment, CancellationToken cancellationToken)
    {
        assignmentRepository.Delete(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}