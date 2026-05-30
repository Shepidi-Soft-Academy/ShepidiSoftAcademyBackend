namespace ShepidiSoft.API.Requests;

public record UpdateDocumentRequest(
int Id,
int DocumentTopicId,
string Title,
string? Description,
string FileUrl);

