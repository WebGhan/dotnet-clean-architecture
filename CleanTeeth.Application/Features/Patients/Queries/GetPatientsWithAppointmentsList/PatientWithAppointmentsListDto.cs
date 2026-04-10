using CleanTeeth.Application.Features.Appointments.Queries.GetAppointmentsList;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsWithAppointmentsList;

public class PatientWithAppointmentsListDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required List<AppointmentsListDto> Appointments { get; set; }
}