using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Instructors.Commands.UpdateInstructor;

public sealed class UpdateInstructorCommandHandler(
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateInstructorCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
    {
        var instructor = await GetInstructorAsync(request.Id);

        if (instructor is null)
            return ServiceResult.Fail("Eğitmen bulunamadı", HttpStatusCode.NotFound);

        ApplyUpdates(instructor, request);
        await PersistAsync(instructor, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Instructor?> GetInstructorAsync(int id)
        => await instructorRepository.GetByIdAsync(id);

    private static void ApplyUpdates(
        Instructor instructor,
        UpdateInstructorCommand request)
    {
        instructor.Bio = request.Bio;
        instructor.Expertise = request.Expertise;
        instructor.IsActive = request.IsActive;
    }

    private async Task PersistAsync(Instructor instructor, CancellationToken cancellationToken)
    {
        instructorRepository.Update(instructor);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}