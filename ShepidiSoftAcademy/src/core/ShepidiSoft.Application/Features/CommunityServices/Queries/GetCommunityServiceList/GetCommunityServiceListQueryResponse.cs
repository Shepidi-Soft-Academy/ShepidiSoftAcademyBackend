namespace ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

public sealed record GetCommunityServiceListQueryResponse(
    int Id,
    string Title,
    string Description,
    string ImageUrl
);