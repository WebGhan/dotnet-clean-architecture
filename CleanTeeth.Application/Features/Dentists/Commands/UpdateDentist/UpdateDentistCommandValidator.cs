using FluentValidation;

namespace CleanTeeth.Application.Features.Dentists.Commands.UpdateDentist;

public class UpdateDentistCommandValidator: AbstractValidator<UpdateDentistCommand>
{
    public UpdateDentistCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required.")
            .EmailAddress()
            .WithMessage("Invalid email address.");
    }
}