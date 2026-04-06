namespace CleanTeeth.Application.Features.Patients.Queries.GetPatientsList;

public class PatientListDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}