using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Enums;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence;
using ShepidiSoft.Persistence.Context;

public sealed class CourseMembershipRepository(AppDbContext context) : GenericRepository<CourseMembership, int>(context), ICourseMembershipRepository
{
  

    public async Task AddInstructorToCourseAsync(int courseId, Guid instructorUserId, CancellationToken cancellationToken)
    {
        var membership = new CourseMembership
        {
            CourseId = courseId,
            UserId = instructorUserId,
            Role = AppRoles.Instructor.ToString(),
            JoinedAt = DateTime.UtcNow
        };

        await context.CourseMemberships.AddAsync(membership, cancellationToken);
    }

    public async Task AddStudentToCourseAsync(int courseId, Guid studentUserId, CancellationToken cancellationToken)
    {
        var membership = new CourseMembership
        {
            CourseId = courseId,
            UserId = studentUserId,
            Role = AppRoles.Student.ToString(),
            JoinedAt = DateTime.UtcNow
        };

        await context.CourseMemberships.AddAsync(membership, cancellationToken);
    }
    public async Task<bool> IsStudentAssignedAsync(int courseId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.CourseMemberships
            .AnyAsync(cm => cm.CourseId == courseId
                         && cm.UserId == userId
                         && cm.Role == "Student", cancellationToken);
    }
    // CourseMembershipRepository.cs
    public async Task<CourseMembership?> GetByUserAndCourseAsync(
      Guid userId,
      int courseId,
      CancellationToken cancellationToken)
    {
        return await context.CourseMemberships
            .FirstOrDefaultAsync(
                cm => cm.UserId == userId && cm.CourseId == courseId && cm.Role == "Student",
                cancellationToken);
    }
}