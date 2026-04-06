using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Commands.UpdateDentist;

public class UpdateDentistCommand: IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}