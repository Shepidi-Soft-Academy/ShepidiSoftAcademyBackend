using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Users.Dtos;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses
{
    public sealed class GetMyCoursesQueryHandler(
        IUserService userService,
        ICourseRepository courseRepository,
        IMapper mapper,
        ICurrentUserService currentUserService
        ) :
        IRequestHandler<GetMyCoursesQuery, ServiceResult<List<GetCourseListQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetCourseListQueryResponse>>> Handle(
        GetMyCoursesQuery request,
        CancellationToken cancellationToken)
        {
            var userId = currentUserService.UserId;
            if (userId is null)
                return ServiceResult<List<GetCourseListQueryResponse>>.Fail("Kullanıcı bulunamadı.");

            var userExists = await userService.IsExistAsync(userId.Value);
            if (!userExists.Data)
                return ServiceResult<List<GetCourseListQueryResponse>>.Fail("User not found.");

            var roles = await userService.GetRolesAsync(userId.Value);
            bool isAdmin = roles.Contains("Admin");

            List<Course> courses = isAdmin
                ? await courseRepository.GetAllCoursesWithInstructorAsync()
                : (await courseRepository.GetCoursesVisibleToUserAsync(userId.Value, cancellationToken)).ToList();

            var instructorUserIds = courses
                .Where(c => c.Instructor != null)
                .Select(c => c.Instructor.UserId)
                .Distinct()
                .ToList();

         
            foreach (var id in instructorUserIds)
                Console.WriteLine($"  InstructorUserId: {id}");

            var usersResult = await userService.GetUsersByIdsAsync(instructorUserIds, cancellationToken);
    
            if (usersResult.Data != null)
                foreach (var u in usersResult.Data)
                    Console.WriteLine($"  User: {u.Id} - {u.FirstName} {u.LastName}");
     

            var userLookup = usersResult.IsSuccess && usersResult.Data is not null
                ? usersResult.Data.ToDictionary(u => u.Id)
                : new Dictionary<Guid, UserDto>();

            var result = courses.Select(c =>
            {
                userLookup.TryGetValue(c.Instructor.UserId, out var user);

                return new GetCourseListQueryResponse(
                    Id: c.Id,
                    Title: c.Title,
                    Instructor: user is not null ? $"{user.FirstName} {user.LastName}" : string.Empty,
                    IsOnline: c.IsOnline,
                    Price: c.Price,
                    Status: c.Status,
                    InstructorId: c.InstructorId,
                    InstructorFullName: user is not null ? $"{user.FirstName} {user.LastName}" : string.Empty,
                    InstructorPhotoUrl: user?.PhotoUrl
                );
            }).ToList();

            return ServiceResult<List<GetCourseListQueryResponse>>.Success(result);
        }
    }
}
