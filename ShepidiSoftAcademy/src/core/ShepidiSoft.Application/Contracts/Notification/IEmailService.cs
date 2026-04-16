namespace ShepidiSoft.Application.Contracts.Notification;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, string? logoPath = null);
    Task SendWithTemplateAsync(string to, string subject, string templateName, Dictionary<string, string> variables, string? logoPath = null);

}