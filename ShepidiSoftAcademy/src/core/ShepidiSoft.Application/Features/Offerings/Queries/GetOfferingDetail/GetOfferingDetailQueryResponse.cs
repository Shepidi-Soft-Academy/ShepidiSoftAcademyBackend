namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingDetail;

public sealed record GetOfferingDetailQueryResponse(
     int Id,
     string Title,
     string Description,
     bool IsActive
    );
