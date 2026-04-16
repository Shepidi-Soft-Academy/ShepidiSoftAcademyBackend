using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShepidiSoft.Application.Contracts.Notification;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Outbox;
using System.Text.Json;

namespace ShepidiSoft.BackgroundJobs.Outbox;

public sealed class OutboxProcessorJob(
    IServiceScopeFactory scopeFactory,
    ILogger<OutboxProcessorJob> logger) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(30);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessPendingMessagesAsync(stoppingToken);
            await Task.Delay(Interval, stoppingToken);
        }
    }

    private async Task ProcessPendingMessagesAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();

        var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        var pendingMessages = await outboxRepository.GetPendingAsync(50, cancellationToken);

        if (pendingMessages.Count == 0)
            return;

        logger.LogInformation("OutboxProcessor: {Count} bekleyen mesaj işleniyor.", pendingMessages.Count);

        foreach (var message in pendingMessages)
        {
            try
            {
                if (message.Type == "Email")
                {
                    var payload = JsonSerializer.Deserialize<EmailOutboxPayload>(message.Payload);

                    if (payload is null)
                        throw new InvalidOperationException("Email payload deserialize edilemedi.");

                    await emailService.SendWithTemplateAsync(
                        to: payload.To,
                        subject: payload.Subject,
                        templateName: payload.TemplateName,
                        variables: payload.Variables,
                        logoPath: payload.LogoPath);
                }

                message.IsSent = true;
                message.ProcessedAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OutboxProcessor: Mesaj gönderilemedi. Id={Id}", message.Id);
                message.Error = ex.Message;
                message.ProcessedAt = DateTime.UtcNow;
            }
        }

        await outboxRepository.SaveChangesAsync(cancellationToken);
    }
}
