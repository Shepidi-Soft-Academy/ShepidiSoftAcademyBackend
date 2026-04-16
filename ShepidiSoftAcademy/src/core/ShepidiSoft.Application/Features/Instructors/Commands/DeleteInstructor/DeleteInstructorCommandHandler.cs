using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Instructors.Commands.DeleteInstructor;

public sealed class DeleteInstructorCommandHandler(
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteInstructorCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        DeleteInstructorCommand request,
        CancellationToken cancellationToken)
    {
        var instructor = await GetInstructorAsync(request.Id);

        if (instructor is null)
            return ServiceResult.Fail("Eğitmen bulunamadı", HttpStatusCode.NotFound);

        await DeleteInstructorAsync(instructor, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Instructor?> GetInstructorAsync(int id)
        => await instructorRepository.GetByIdAsync(id);

    private async Task DeleteInstructorAsync(Instructor instructor, CancellationToken cancellationToken)
    {
        instructorRepository.Delete(instructor);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}