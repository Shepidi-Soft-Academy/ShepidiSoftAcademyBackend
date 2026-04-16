using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShepidiSoft.Application.Contracts.Notification;
using ShepidiSoft.Notification.Mail;
using ShepidiSoft.Notification.Options;

namespace ShepidiSoft.Notification;

public static class DependencyInjection
{

    public static IServiceCollection AddNotificationExt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(
          configuration.GetSection("EmailSettings"));

        services.AddScoped<IEmailService, EmailService>();
   




        return services;
    }

}
