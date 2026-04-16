using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface ICourseMembershipRepository : IGenericRepository<CourseMembership, int>
{
    Task AddInstructorToCourseAsync(int courseId, Guid instructorUserId, CancellationToken cancellationToken);
    Task AddStudentToCourseAsync(int courseId, Guid studentUserId, CancellationToken cancellationToken);
    Task<bool> IsStudentAssignedAsync(int courseId, Guid userId, CancellationToken cancellationToken);
    // ICourseMembershipRepository.cs
    Task<CourseMembership?> GetByUserAndCourseAsync(Guid userId, int courseId, CancellationToken cancellationToken);
}