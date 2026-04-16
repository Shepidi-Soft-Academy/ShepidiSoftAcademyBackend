using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Announcements;

public sealed class AnnouncementRepository(AppDbContext context) : GenericRepository<Announcement, int>(context), IAnnouncementRepository
{
}
