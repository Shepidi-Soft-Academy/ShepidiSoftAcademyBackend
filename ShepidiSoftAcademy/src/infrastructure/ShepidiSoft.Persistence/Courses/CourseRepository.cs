using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Courses;

public sealed class CourseRepository(AppDbContext context)
    : GenericRepository<Course, int>(context), ICourseRepository
{
    // Normal kullanıcılar için: sadece kendisine görünür kurslar
    public async Task<IReadOnlyList<Course>> GetCoursesVisibleToUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Courses
            .Where(c => c.Memberships.Any(m => m.UserId == userId))
            .Include(c => c.Memberships)      // Kurs üyeliklerini çek
            .Include(c => c.Instructor)       // Instructor bilgisini çek
            .AsNoTracking()                    // Performans için readonly
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Course>> GetAllCoursesWithInstructorAsync()
    {
        return await context.Courses
            .Include(c => c.Instructor)       // Instructor ilişkisini çek
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Course> GetByIdWithInstructorAsync(int id)
    {

        return await context.Courses
            .Include(c => c.Instructor)  
            .Where(c=>c.Id==id)// Instructor ilişkisini çek
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}