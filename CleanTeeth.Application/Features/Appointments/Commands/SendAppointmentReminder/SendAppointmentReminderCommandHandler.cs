using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Contracts.Repositories.Models;
using CleanTeeth.Application.Notifications;
using CleanTeeth.Application.Utilities;
using CleanTeeth.Domain.Enums;

namespace CleanTeeth.Application.Features.Appointments.Commands.SendAppointmentReminder;

public class SendAppointmentReminderCommandHandler : IRequestHandler<SendAppointmentReminderCommand>
{
    private readonly IAppointmentRepository _repository;
    private readonly INotifications _notifications;

    public SendAppointmentReminderCommandHandler(IAppointmentRepository repository, INotifications notifications)
    {
        _repository = repository;
        _notifications = notifications;
    }

    public async Task Handle(SendAppointmentReminderCommand request)
    {
        var startDate = DateTime.UtcNow.Date.AddDays(1);
        var endDate = startDate.AddDays(1);

        var filter = new AppointmentsFilterDto
        {
            StartDate = startDate,
            EndDate = endDate,
            AppointmentStatus = AppointmentStatus.Scheduled
        };

        var appointments = await _repository.GetFiltered(filter);

        foreach (var appointment in appointments)
        {
            var dto = appointment.toDto();
            await _notifications.SendAppointmentReminder(dto);
            
        }
    }
}