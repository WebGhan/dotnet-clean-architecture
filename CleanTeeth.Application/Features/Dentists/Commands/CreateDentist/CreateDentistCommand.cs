using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Commands.CreateDentist;

public class CreateDentistCommand : IRequest<Guid>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}