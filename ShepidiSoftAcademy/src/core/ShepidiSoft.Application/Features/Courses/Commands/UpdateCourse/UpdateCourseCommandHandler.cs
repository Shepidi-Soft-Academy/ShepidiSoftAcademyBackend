using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandHandler(
    ICourseRepository courseRepository,
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = await GetCourseAsync(request.Id);

        if (course is null)
            return ServiceResult.Fail("Kurs bulunamadı", HttpStatusCode.NotFound);

        if (course.InstructorId != request.InstructorId)
        {
            var instructorExists = await instructorRepository
                .AnyAsync(x => x.Id == request.InstructorId);

            if (!instructorExists)
                return ServiceResult.Fail("Eğitmen bulunamadı", HttpStatusCode.BadRequest);
        }

        ApplyUpdates(course, request);

        await PersistAsync(course, cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    private async Task<Course?> GetCourseAsync(int id)
        => await courseRepository.GetByIdAsync(id);

    private static void ApplyUpdates(
        Course course,
        UpdateCourseCommand request)
    {
        course.Title = request.Title;
        course.Description = request.Description;
        course.Location = request.Location;
        course.InstructorId = request.InstructorId;
        course.IsOnline = request.IsOnline;
        course.MeetingUrl = request.MeetingUrl;
        course.CoverImageUrl = request.CoverImageUrl;
        course.DurationInWeeks = request.DurationInWeeks;
        course.StartedDate = request.StartedDate;
        course.EndDate = request.EndDate;
        course.Status = request.Status;
        course.Price = request.Price;
    }

    private async Task PersistAsync(Course course, CancellationToken token)
    {
        courseRepository.Update(course);
        await unitOfWork.SaveChangesAsync(token);
    }
}