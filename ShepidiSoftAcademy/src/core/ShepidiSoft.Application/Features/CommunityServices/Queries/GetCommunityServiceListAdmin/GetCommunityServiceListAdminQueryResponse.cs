namespace ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

public sealed record GetCommunityServiceListAdminQueryResponse(
    int Id,
    string Title,
    bool IsActive,
    string Description,
    string CreatedByName
    );
