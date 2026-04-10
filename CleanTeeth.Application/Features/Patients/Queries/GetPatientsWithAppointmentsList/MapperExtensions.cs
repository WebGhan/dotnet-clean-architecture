using CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsWithAppointmentsList;

internal static class MapperExtensions
{
    internal static PatientWithAppointmentsListDto ToDtoWithAppointments(this Patient patient, List<Appointment> appointments)
    {
        var appointmentDtos = appointments.Select(appointment => appointment.ToDto()).ToList();

        return new PatientWithAppointmentsListDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Email = patient.Email.Value,
            Appointments = appointmentDtos
        };
    }
}