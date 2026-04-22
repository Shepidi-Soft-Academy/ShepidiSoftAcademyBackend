using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Meetings;

public sealed class MeetingRepository(AppDbContext context) : GenericRepository<Meeting, int>(context), IMeetingRepository
{
}
