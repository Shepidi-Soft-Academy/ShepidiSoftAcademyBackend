using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.CommunityServices;

public sealed class CommunityServiceRepository(AppDbContext context) : GenericRepository<CommunityService, int>(context), ICommunityServiceRepository
{
 
}

