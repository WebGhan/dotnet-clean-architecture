using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.UpdateDentalOffice;

public class UpdateDentalOfficeCommand: IRequest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}