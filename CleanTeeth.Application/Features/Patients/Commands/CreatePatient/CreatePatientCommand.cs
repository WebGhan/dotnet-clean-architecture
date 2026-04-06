using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommand: IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}