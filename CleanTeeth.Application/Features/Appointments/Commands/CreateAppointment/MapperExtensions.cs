using CleanTeeth.Application.Notifications;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Appointments.Commands.CreateAppointment;

internal static class MapperExtensions
{
    internal static AppointmentConfirmationDto ToDto(this Appointment appointment)
    {
        return new AppointmentConfirmationDto
        {
            Id = appointment.Id,
            Date = appointment.TimeInterval.Start,
            Patient = appointment.Patient!.Name,
            PatientEmail = appointment.Patient!.Email.Value,
            DentalOffice = appointment.DentalOffice!.Name,
            Dentist = appointment.Dentist!.Name
        };
    }
}