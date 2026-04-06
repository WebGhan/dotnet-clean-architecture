using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.Dentists.Commands.DeleteDentist;

public class DeleteDentistCommand: IRequest
{
    public required Guid Id { get; set; }
}