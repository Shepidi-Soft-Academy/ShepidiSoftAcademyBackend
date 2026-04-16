using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Students;

public sealed class StudentRepository(AppDbContext context) : GenericRepository<Student, Guid>(context), IStudentRepository
{
    public async Task<ServiceResult<bool>> CheckIsInstructorExistByUserId(Guid userId)
    {
        var exists = await context.Students.AnyAsync(i => i.UserId == userId);
        return ServiceResult<bool>.Success(exists);
    }

    public async Task<List<Student>> GetStudentsByCourseIdAsync(int courseId, CancellationToken cancellationToken)
    {
        return await context.CourseMemberships
        .Where(cm => cm.CourseId == courseId && cm.Role == "Student")
        .Join(
            context.Students,
            cm => cm.UserId,
            s => s.UserId,
            (cm, s) => s
        )
        .ToListAsync(cancellationToken);
    }
    public async Task<ServiceResult<Student>> GetByUserId(Guid userId)
    {
        var student = await context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null)
            return ServiceResult<Student>.Fail("Öğrenci bulunamadı.");

        return ServiceResult<Student>.Success(student);
    }
}
