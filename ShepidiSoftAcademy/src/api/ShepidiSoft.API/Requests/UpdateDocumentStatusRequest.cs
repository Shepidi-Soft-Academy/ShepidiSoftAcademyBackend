using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API.Requests;


public record UpdateDocumentStatusRequest(
    int Id,
    DocumentStatus NewStatus
);