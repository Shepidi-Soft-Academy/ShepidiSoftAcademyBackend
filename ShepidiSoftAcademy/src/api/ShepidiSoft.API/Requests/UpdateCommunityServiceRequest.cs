namespace ShepidiSoft.API.Requests;

public sealed record UpdateCommunityServiceRequest(
    string Title,
    string Description,
    bool IsActive,
    string? ImageUrl);
