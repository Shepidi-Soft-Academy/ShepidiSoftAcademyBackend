using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface ICareerApplicationRepository : IGenericRepository<CareerApplication, int>
{
    Task<List<CareerApplication>> GetAllCareerApplicationsWithPositionAsync();
    Task<CareerApplication?> GetCareerApplicationWithPositionAsync(int id);

}
