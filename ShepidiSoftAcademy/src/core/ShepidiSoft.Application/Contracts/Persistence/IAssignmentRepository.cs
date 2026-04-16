

using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface IAssignmentRepository : IGenericRepository<Assignment, int>
{
   
 Task<IReadOnlyList<Assignment>>GetByCourseIdAsync(int courseId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Assignment>> GetAssignmentsForStudentAsync(Guid studentId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Assignment>> GetAssignmentsForInstructorAsync(int instructorId, CancellationToken cancellationToken);

}
