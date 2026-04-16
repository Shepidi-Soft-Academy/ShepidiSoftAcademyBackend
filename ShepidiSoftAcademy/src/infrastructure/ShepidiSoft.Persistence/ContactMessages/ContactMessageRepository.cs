using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.ContactMessages;

public sealed class ContactMessageRepository(AppDbContext context) : GenericRepository<ContactMessage, int>(context), IContactMessageRepository
{
}
