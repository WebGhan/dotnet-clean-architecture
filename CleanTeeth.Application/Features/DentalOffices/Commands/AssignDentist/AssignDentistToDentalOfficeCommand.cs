using CleanTeeth.Application.Utilities;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.AssignDentist;

public class AssignDentistToDentalOfficeCommand : IRequest
{
    public Guid DentalOfficeId { get; set; }
    public Guid DentistId { get; set; }
}