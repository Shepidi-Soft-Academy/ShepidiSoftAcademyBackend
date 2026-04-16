using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Documents;

public sealed class DocumentRepository(AppDbContext context) : GenericRepository<Document, int>(context),IDocumentRepository
{
}
