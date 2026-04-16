namespace ShepidiSoft.Application.Features.OrganizationMembers.Queries.GetOrganizationMemberList;

public sealed record GetOrganizationMemberListQueryResponse(
    string FullName,
    IReadOnlyList<string> Positions,  
    string LinkednUrl,
    string PhotoUrl
);