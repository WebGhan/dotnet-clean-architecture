using CleanTeeth.Application.Features.Appointments.Commands.SendAppointmentReminder;
using CleanTeeth.Application.Utilities;

namespace CleanTeeth.API.Jobs;

public class AppointmentsReminderJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly TimeZoneInfo _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public AppointmentsReminderJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZoneInfo);

            if (now.Hour == 8)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new SendAppointmentReminderCommand());
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}