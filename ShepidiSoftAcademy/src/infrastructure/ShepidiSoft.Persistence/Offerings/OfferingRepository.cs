using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Offerings;

public sealed class OfferingRepository(AppDbContext context) : GenericRepository<Offering, int>(context), IOfferingRepository
{
}
