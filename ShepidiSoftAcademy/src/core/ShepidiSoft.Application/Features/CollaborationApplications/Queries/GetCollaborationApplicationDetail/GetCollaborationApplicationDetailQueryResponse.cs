using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationDetail;

public sealed record GetCollaborationApplicationDetailQueryResponse(
    int Id,
    string Title,
    string Description,
    string CommunityName,
    string ContactName,
    string ContactEmail,
    string ContactPhone,
    CollaborationApplicationStatus Status
    );

