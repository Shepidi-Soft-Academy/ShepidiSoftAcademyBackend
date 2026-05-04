using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationList;

public sealed record GetCollaborationApplicationListQueryResponse(
    int Id,
    string Title,
    string CommunityName,
    string ContactName,
    string ContactEmail,
    string ContactPhone,
    CollaborationApplicationStatus Status
    );

