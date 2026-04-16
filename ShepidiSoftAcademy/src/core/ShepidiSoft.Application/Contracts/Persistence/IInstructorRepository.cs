using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorDetail;
using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorListWithUser;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface IInstructorRepository : IGenericRepository<Instructor, int>
{
    Task<ServiceResult<bool>> CheckIsInstructorExistByUserId(Guid userId);
    Task<List<GetInstructorListWithUserQueryResponse>> GetListAsync();
    Task<ServiceResult<int>> GetInstructorIdByUserId(Guid userId);

    // Explicitly hide the inherited GetByIdAsync from IGenericRepository to avoid CS0108.
    // If you prefer to keep both accessible, consider renaming this method (e.g. GetDetailByIdAsync).
  Task<GetInstructorDetailWithUserQueryResponse?> GetInstructorDetailAsync(int Id);
    Task<Guid?> GetUserIdByInstructorIdAsync(int instructorId, CancellationToken cancellationToken);
    

}
