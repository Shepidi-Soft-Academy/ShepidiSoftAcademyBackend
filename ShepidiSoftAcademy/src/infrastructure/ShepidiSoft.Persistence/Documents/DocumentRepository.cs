using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.Documents;

public sealed class DocumentRepository(AppDbContext context) : GenericRepository<Document, int>(context), IDocumentRepository
{
    public async Task<List<Document>> GetDocumentsWithTopicsAsync()
    {
        return await context.Documents
            .Include(d => d.DocumentTopic)
            .ToListAsync();

    }
}
