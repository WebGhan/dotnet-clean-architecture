using CleanTeeth.Application.Utilities.Common;

namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;

public class PatientsFilterDto : PagedFilterDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}