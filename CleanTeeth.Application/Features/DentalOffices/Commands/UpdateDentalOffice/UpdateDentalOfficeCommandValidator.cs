using FluentValidation;

namespace CleanTeeth.Application.Features.DentalOffices.Commands.UpdateDentalOffice;

public class UpdateDentalOfficeCommandValidator: AbstractValidator<UpdateDentalOfficeCommand>
{
    public UpdateDentalOfficeCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("The filed {PropertyName} is required.");
    }
}