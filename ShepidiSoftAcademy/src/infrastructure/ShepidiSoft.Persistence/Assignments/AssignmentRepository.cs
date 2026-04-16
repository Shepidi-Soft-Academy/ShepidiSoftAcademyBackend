using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Assignments;

public class AssignmentRepository(AppDbContext context) : GenericRepository<Assignment, int>(context), IAssignmentRepository
{
    public async Task<IReadOnlyList<Assignment>> GetByCourseIdAsync(int courseId, CancellationToken cancellationToken)
    {
        return await context.Assigments
      .Where(x => x.CourseId == courseId)
      .ToListAsync(cancellationToken);
    }
    public async Task<IReadOnlyList<Assignment>> GetAssignmentsForStudentAsync(
      Guid studentId,
      CancellationToken cancellationToken)
    {
        var student = await context.Students
            .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken);

        if (student is null)
            return new List<Assignment>();

        return await context.Assigments
            .Include(a => a.Course)
            .Where(a => a.IsActive &&
                a.Course.Memberships.Any(m =>
                    m.UserId == student.UserId && m.Role == "Student"))
            .ToListAsync(cancellationToken);
    }
    public async Task<IReadOnlyList<Assignment>> GetAssignmentsForInstructorAsync(int instructorId, CancellationToken cancellationToken)
    {
        return await context.Assigments
            .Include(a => a.Course)
            .Where(a => a.Course.InstructorId == instructorId && a.IsActive)
            .ToListAsync(cancellationToken);
    }
}
