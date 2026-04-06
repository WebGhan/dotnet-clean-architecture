using CleanTeeth.Application.Notifications;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Appointments.Commands.SendAppointmentReminder;

internal static class MapperExtensions
{
    internal static AppointmentReminderDto toDto(this Appointment appointment)
    {
        return new AppointmentReminderDto
        {
            Id = appointment.Id,
            Date = appointment.TimeInterval.Start,
            Patient = appointment.Patient!.Name,
            PatientEmail = appointment.Patient!.Email.Value,
            Dentist = appointment.Dentist!.Name,
            DentalOffice = appointment.DentalOffice!.Name,
        };
    }
}