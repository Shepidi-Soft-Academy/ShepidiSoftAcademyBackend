namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingList;

public sealed record GetOfferingListQueryResponse(
    int Id,
    string Title,
    string isActive
    );
