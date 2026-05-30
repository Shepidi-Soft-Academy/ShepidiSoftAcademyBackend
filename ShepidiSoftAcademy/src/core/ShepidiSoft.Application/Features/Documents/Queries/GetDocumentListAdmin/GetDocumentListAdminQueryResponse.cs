namespace ShepidiSoft.Application.Features.Documents.Queries.GetDocumentListAdmin;

public record GetDocumentListAdminQueryResponse(
 int Id,
 string DocumentTopicName,
 string Title,
 string? Description,
 string FileUrl,
 string CreatedByMail,
 DateTime CreatedTime
);