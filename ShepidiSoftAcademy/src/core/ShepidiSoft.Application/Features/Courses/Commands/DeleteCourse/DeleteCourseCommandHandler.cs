using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Courses.Commands.DeleteCourse;

public sealed class DeleteCourseCommandHandler(
    ICourseRepository courseRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        DeleteCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await GetCourseAsync(request.Id);

        if (course is null)
            return ServiceResult.Fail("Kurs bulunamadı", HttpStatusCode.NotFound);

        await DeleteCourseAsync(course, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Course?> GetCourseAsync(int id)
        => await courseRepository.GetByIdAsync(id);

    private async Task DeleteCourseAsync(Course course, CancellationToken cancellationToken)
    {
        courseRepository.Delete(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}