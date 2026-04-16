namespace ShepidiSoft.API;

public sealed record UpdateActivityRequest(
    string Title,
    string Description,
    DateTime Date,
    bool IsOnline,
    string Location,
    string OnlineMeetingUrl);

