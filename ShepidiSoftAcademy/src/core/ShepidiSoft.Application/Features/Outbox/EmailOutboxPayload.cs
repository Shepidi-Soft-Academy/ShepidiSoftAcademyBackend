namespace ShepidiSoft.Application.Features.Outbox;

public sealed class EmailOutboxPayload
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string TemplateName { get; set; } = string.Empty;
    public Dictionary<string, string> Variables { get; set; } = [];
    public string? LogoPath { get; set; }
}
