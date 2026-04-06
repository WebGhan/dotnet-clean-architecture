using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommand: IRequest
{
    public required Guid Id { get; set; }
}