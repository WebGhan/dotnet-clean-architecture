using CleanTeeth.Application.Notifications;
using CleanTeeth.Infrastructure.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Infrastructure;

public static class RegisterInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<INotifications, EmailService>();
        
        return services;
    }
}