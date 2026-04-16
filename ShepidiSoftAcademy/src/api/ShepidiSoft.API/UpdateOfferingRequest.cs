namespace ShepidiSoft.API;

public sealed record UpdateOfferingRequest
    (
     string Title,
     string Description,
     bool IsActive
    );
