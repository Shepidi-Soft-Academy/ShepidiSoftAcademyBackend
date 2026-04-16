using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.Courses.Queries.GetCourseDetail;

public sealed class GetCourseDetailQueryHandler(
    ICourseRepository courseRepository,
    IUserService userService, 
    IMapper mapper
    ) : IRequestHandler<GetCourseDetailQuery, ServiceResult<GetCourseDetailQueryResponse>>
{
    public async Task<ServiceResult<GetCourseDetailQueryResponse>> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
       
        var course = await courseRepository.GetByIdWithInstructorAsync(request.Id);

        if (course is null)
            return ServiceResult<GetCourseDetailQueryResponse>.Fail("Kurs Bulunamadı", HttpStatusCode.NotFound);

        string instructorName = "Bilinmeyen Eğitmen";
        string instructorPhotoUrl = "photourl";

     
        if (course.Instructor != null)
        {
            
            var instructorIds = new List<Guid> { course.Instructor.UserId };
            var usersResult = await userService.GetUsersByIdsAsync(instructorIds, cancellationToken);

           
            if (usersResult.IsSuccess && usersResult.Data?.Count > 0)
            {
                var user = usersResult.Data.First();
                instructorName = $"{user.FirstName} {user.LastName}";
                instructorPhotoUrl = user.PhotoUrl;

            }
        }

        
        var response = new GetCourseDetailQueryResponse(
            Title: course.Title,
            Description: course.Description,
            Location: course.Location,
            InstructorFullName: instructorName,
            IsOnline: course.IsOnline,
            MeetingUrl: course.MeetingUrl,
            CoverImageUrl: course.CoverImageUrl,
            DurationInWeeks: course.DurationInWeeks,
            StartedDate: course.StartedDate,
            EndDate: course.EndDate,
            Status: course.Status.ToString(),
            Price: course.Price,
            InstructorPhotoUrl:instructorPhotoUrl,
            ApplicationFormUrl:course.ApplicationFormUrl
        );

        return ServiceResult<GetCourseDetailQueryResponse>.Success(response);
    }
}