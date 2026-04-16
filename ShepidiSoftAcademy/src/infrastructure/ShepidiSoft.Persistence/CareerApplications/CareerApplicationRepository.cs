using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.CareerApplications;

public sealed class CareerApplicationRepository(AppDbContext context) : GenericRepository<CareerApplication, int>(context), ICareerApplicationRepository
{
    public Task<List<CareerApplication>> GetAllCareerApplicationsWithPositionAsync() =>
      Context.CareerApplications
          .Include(x => x.OrganizationPosition)
          .AsNoTracking()
          .ToListAsync();

    public Task<CareerApplication?> GetCareerApplicationWithPositionAsync(int id) =>
    Context.CareerApplications
        .Include(x => x.OrganizationPosition)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);


}
