using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Courses.Queries.GetCourses;
using ShepidiSoft.Application.Features.Users.Dtos;

public sealed class GetCoursesQueryHandler(
    IUserService userService,
    ICourseRepository courseRepository
) : IRequestHandler<GetCoursesQuery, ServiceResult<List<GetCoursesQueryResponse>>>
{
    public async Task<ServiceResult<List<GetCoursesQueryResponse>>> Handle(
        GetCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAllCoursesWithInstructorAsync();

        var instructorUserIds = courses
            .Where(c => c.Instructor != null)
            .Select(c => c.Instructor.UserId)
            .Distinct()
            .ToList();

        var usersResult = await userService.GetUsersByIdsAsync(instructorUserIds, cancellationToken);

        var userLookup = usersResult.IsSuccess && usersResult.Data is not null
            ? usersResult.Data.ToDictionary(u => u.Id)
            : new Dictionary<Guid, UserDto>();

        var result = courses.Select(c =>
        {
            UserDto? user = null;
            if (c.Instructor != null)
                userLookup.TryGetValue(c.Instructor.UserId, out user);

            return new GetCoursesQueryResponse(
                Id: c.Id,
                Title: c.Title,
                IsOnline: c.IsOnline,
                Price: c.Price,
                Status: c.Status,
                InstructorFullName: user is not null ? $"{user.FirstName} {user.LastName}" : string.Empty,
                CoverImageUrl:c.CoverImageUrl,
                ApplicationFormUrl:c.ApplicationFormUrl
                
            );
        }).ToList();

        return ServiceResult<List<GetCoursesQueryResponse>>.Success(result);
    }
}