using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Students.Commands.RemoveStudentFromCourse;

public sealed class RemoveStudentFromCourseCommandHandler(
    ICourseMembershipRepository membershipRepository,
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RemoveStudentFromCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        RemoveStudentFromCourseCommand request,
        CancellationToken cancellationToken)
    {
        var courseExists = await courseRepository.AnyAsync(c => c.Id == request.CourseId);
        if (!courseExists)
            return ServiceResult.Fail("Kurs bulunamadı.", HttpStatusCode.NotFound);

        var membership = await membershipRepository
            .GetByUserAndCourseAsync(request.UserId, request.CourseId, cancellationToken);

        if (membership is null)
            return ServiceResult.Fail("Öğrenci bu kursa kayıtlı değil.", HttpStatusCode.NotFound);

         membershipRepository.Delete(membership);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success();
    }
}