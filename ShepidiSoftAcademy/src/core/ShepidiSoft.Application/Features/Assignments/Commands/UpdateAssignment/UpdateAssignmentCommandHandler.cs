using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Assignments.Commands.UpdateAssignment;

public sealed class UpdateAssignmentCommandHandler(
    IAssignmentRepository assignmentRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateAssignmentCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await GetAssignmentAsync(request.Id);

        if (assignment is null)
            return ServiceResult.Fail("Ödev bulunamadı", HttpStatusCode.NotFound);

        ApplyUpdates(assignment, request);
        await PersistAsync(assignment, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Assignment?> GetAssignmentAsync(int id)
        => await assignmentRepository.GetByIdAsync(id);

    private static void ApplyUpdates(
        Assignment assignment,
        UpdateAssignmentCommand request)
    {
        assignment.Title = request.Title;
        assignment.Description = request.Description;
        assignment.DueDate = request.DueDate;
        assignment.CourseId = request.CourseId;
    }

    private async Task PersistAsync(Assignment assignment, CancellationToken token)
    {
        assignmentRepository.Update(assignment);
        await unitOfWork.SaveChangesAsync(token);
    }
}