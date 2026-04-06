using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.DeleteDentalOffice;

public class DeleteDentalOfficeCommand: IRequest
{
    public required Guid Id { get; set; }
}