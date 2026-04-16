using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.Persistence.DocumentTopics;

public sealed class DocumentTopicRepository(AppDbContext context) : GenericRepository<DocumentTopic, int>(context), IDocumentTopicRepository
{
}
