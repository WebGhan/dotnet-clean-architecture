using CleanTeeth.Domain.Entites;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;

internal static class MapperExtensions
{
    internal static AppointmentsListDto ToDto(this Appointment appointment)
    {
        return new AppointmentsListDto
        {
            Id = appointment.Id,
            StartDate = appointment.TimeInterval.Start,
            EndDate = appointment.TimeInterval.End,
            DentalOffice = appointment.DentalOffice!.Name,
            Dentist = appointment.Dentist!.Name,
            Patient = appointment.Patient!.Name,
            Status = appointment.Status.ToString()
        };
    }
}