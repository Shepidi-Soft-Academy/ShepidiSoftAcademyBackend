namespace ShepidiSoft.API;

public sealed record UpdateOrganizationMemberRequest(
    IReadOnlyList<int> PositionIds
);