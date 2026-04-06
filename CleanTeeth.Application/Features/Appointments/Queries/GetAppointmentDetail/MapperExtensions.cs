using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentDetail;

internal static class MapperExtensions
{
    internal static AppointmentDetailDto ToDto(this Appointment appointment)
    {
        return new AppointmentDetailDto
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