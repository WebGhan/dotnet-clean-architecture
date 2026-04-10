using FluentValidation;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.AssignDentist;

public class AssignDentistToDentalOfficeCommandValidator : AbstractValidator<AssignDentistToDentalOfficeCommand>
{
    public AssignDentistToDentalOfficeCommandValidator()
    {
        RuleFor(cmd => cmd.DentalOfficeId)
            .NotEmpty().WithMessage("Dental office ID is required.");

        RuleFor(cmd => cmd.DentistId)
            .NotEmpty().WithMessage("Dentist ID is required.");
    }
}