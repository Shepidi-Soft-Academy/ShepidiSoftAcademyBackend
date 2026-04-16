using ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface ICourseRepository:IGenericRepository<Course,int>
{
    Task<IReadOnlyList<Course>> GetCoursesVisibleToUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Course>> GetAllCoursesWithInstructorAsync();
    Task<Course> GetByIdWithInstructorAsync(int id);

}
