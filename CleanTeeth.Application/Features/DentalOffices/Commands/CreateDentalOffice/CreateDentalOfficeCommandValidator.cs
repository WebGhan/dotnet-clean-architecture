using FluentValidation;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandValidator: AbstractValidator<CreateDentalOfficeCommand>
{
    public CreateDentalOfficeCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("The filed {PropertyName} is required.");
    }
}