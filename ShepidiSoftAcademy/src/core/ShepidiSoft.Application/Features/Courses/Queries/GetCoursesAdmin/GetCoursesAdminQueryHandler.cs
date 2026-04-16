using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Courses.Queries.GetCoursesAdmin;
using ShepidiSoft.Application.Features.Users.Dtos;

public sealed class GetCoursesAdminQueryHandler(
    IUserService userService,
    ICourseRepository courseRepository
) : IRequestHandler<GetCoursesAdminQuery, ServiceResult<List<GetCoursesAdminQueryResponse>>>
{
    public async Task<ServiceResult<List<GetCoursesAdminQueryResponse>>> Handle(
        GetCoursesAdminQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAllCoursesWithInstructorAsync();

        // 🔹 Instructor userId'leri
        var instructorUserIds = courses
            .Where(c => c.Instructor != null)
            .Select(c => c.Instructor!.UserId);

        // 🔹 Audit userId'leri (CreatedBy + UpdatedBy)
        var auditUserIds = courses
            .SelectMany(c => new Guid?[]
            {
                c.CreatedBy,
                c.UpdatedBy
            })
            .Where(x => x.HasValue)
            .Select(x => x!.Value);

        // 🔹 Hepsini birleştir (N+1 önleme)
        var allUserIds = instructorUserIds
            .Concat(auditUserIds)
            .Distinct()
            .ToList();

        var usersResult = await userService.GetUsersByIdsAsync(allUserIds, cancellationToken);

        var userLookup = usersResult.IsSuccess && usersResult.Data is not null
            ? usersResult.Data.ToDictionary(u => u.Id)
            : new Dictionary<Guid, UserDto>();

        // 🔹 Helper (clean code)
        UserDto? GetUser(Guid? id)
        {
            if (!id.HasValue) return null;
            return userLookup.TryGetValue(id.Value, out var user) ? user : null;
        }

        var result = courses.Select(c =>
        {
            var instructorUser = c.Instructor != null
                ? GetUser(c.Instructor.UserId)
                : null;

            var createdByUser = GetUser(c.CreatedBy);
            var updatedByUser = GetUser(c.UpdatedBy);

            return new GetCoursesAdminQueryResponse(
                Id: c.Id,
                Title: c.Title,
                IsOnline: c.IsOnline,
                Price: c.Price,
                Status: c.Status,

                InstructorFullName: instructorUser is not null
                    ? $"{instructorUser.FirstName} {instructorUser.LastName}"
                    : string.Empty,

                CoverImageUrl: c.CoverImageUrl,
                ApplicationFormUrl: c.ApplicationFormUrl,

                CreatedDate: c.Created,
                LastUpdateDate: c.Updated,

                CreatedByUserId: c.CreatedBy,
                CreatedByUserName: createdByUser is not null
                    ? $"{createdByUser.FirstName} {createdByUser.LastName}"
                    : string.Empty,
                CreatedByEmail: createdByUser?.Email ?? string.Empty,

                UpdatedByUserId: c.UpdatedBy,
                UpdatedByUserName: updatedByUser is not null
                    ? $"{updatedByUser.FirstName} {updatedByUser.LastName}"
                    : null,
                UpdatedByEmail: updatedByUser?.Email
            );
        }).ToList();

        return ServiceResult<List<GetCoursesAdminQueryResponse>>.Success(result);
    }
}