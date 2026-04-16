

using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface IStudentRepository:IGenericRepository<Student,Guid>
{
    Task<ServiceResult<bool>> CheckIsInstructorExistByUserId(Guid userId);
    Task<List<Student>> GetStudentsByCourseIdAsync(int courseId, CancellationToken cancellationToken);
    Task<ServiceResult<Student>> GetByUserId(Guid userId);


}
