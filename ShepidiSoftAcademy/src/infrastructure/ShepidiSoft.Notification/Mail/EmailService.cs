using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ShepidiSoft.Application.Contracts.Notification;
using ShepidiSoft.Notification.Options;

namespace ShepidiSoft.Notification.Mail;

public sealed class EmailService(IOptions<EmailSettings> options) : IEmailService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendAsync(string to, string subject, string body, string? logoPath = null)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        var builder = new BodyBuilder();

        if (logoPath != null && File.Exists(logoPath))
        {
            var image = builder.LinkedResources.Add(logoPath);
            image.ContentId = "logo";
            body = body.Replace("{{LogoUrl}}", $"cid:logo");
        }

        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
    public async Task SendWithTemplateAsync(string to, string subject, string templateName, Dictionary<string, string> variables, string? logoPath = null)
    {
        var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", $"{templateName}.html");
        var body = await File.ReadAllTextAsync(templatePath);

        body = variables.Aggregate(body, (current, variable) =>
            current.Replace($"{{{{{variable.Key}}}}}", variable.Value));

        await SendAsync(to, subject, body, logoPath);
    }
}
