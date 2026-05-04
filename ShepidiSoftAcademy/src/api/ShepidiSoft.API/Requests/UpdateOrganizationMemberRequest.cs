namespace ShepidiSoft.API.Requests;

public sealed record UpdateOrganizationMemberRequest(
    IReadOnlyList<int> PositionIds
);