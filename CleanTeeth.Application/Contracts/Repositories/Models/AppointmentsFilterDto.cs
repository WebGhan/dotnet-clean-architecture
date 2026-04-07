using CleanTeeth.Application.Utilities.Common;
using CleanTeeth.Domain.Enums;

namespace CleanTeeth.Application.Contracts.Repositories.Models;

public class AppointmentsFilterDto : PagedFilterDto
{
    public Guid? PatientId { get; set; }
    public Guid? DentistId { get; set; }
    public Guid? DentalOfficeId { get; set; }
    public AppointmentStatus? AppointmentStatus { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}