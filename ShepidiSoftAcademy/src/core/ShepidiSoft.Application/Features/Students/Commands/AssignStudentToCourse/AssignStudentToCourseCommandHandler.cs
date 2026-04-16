using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Students.Commands.AssignStudentToCourse;

public sealed class AssignStudentToCourseCommandHandler(
    ICourseRepository courseRepository,
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork,
    ICourseMembershipRepository membershipRepository
) : IRequestHandler<AssignStudentToCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        AssignStudentToCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.CourseId);

        if (course is null)
            return ServiceResult.Fail("Kurs bulunamadı", HttpStatusCode.NotFound);

        if (course.Status != "Active")
            return ServiceResult.Fail("Kurs aktif değil", HttpStatusCode.BadRequest);

        var student = await studentRepository.GetByIdAsync(request.StudentId);

        if (student is null)
            return ServiceResult.Fail("Öğrenci bulunamadı", HttpStatusCode.NotFound);

        var isAlreadyAssigned = await membershipRepository.IsStudentAssignedAsync(
            request.CourseId, student.UserId, cancellationToken);

        if (isAlreadyAssigned)
            return ServiceResult.Fail("Öğrenci zaten bu kursa kayıtlı", HttpStatusCode.Conflict);

        var membership = new CourseMembership
        {
            CourseId = request.CourseId,
            UserId = student.UserId,
            Role = "Student",
            JoinedAt = DateTime.UtcNow
        };

        await membershipRepository.AddAsync(membership);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.Created);
    }
}