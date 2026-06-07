using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Contracts.Persistence;

public interface IDocumentRepository:IGenericRepository<Document,int>
{
    Task<List<Document>> GetDocumentsWithTopicsAsync();
}
