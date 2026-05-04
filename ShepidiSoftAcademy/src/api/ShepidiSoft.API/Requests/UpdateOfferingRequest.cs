namespace ShepidiSoft.API.Requests;

public sealed record UpdateOfferingRequest
    (
     string Title,
     string Description,
     bool IsActive
    );
