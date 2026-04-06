using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommand: IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}