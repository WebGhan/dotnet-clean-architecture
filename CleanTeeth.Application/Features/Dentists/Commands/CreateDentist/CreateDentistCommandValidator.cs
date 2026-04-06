using FluentValidation;

namespace CleanTeeth.Application.Features.Dentists.Commands.CreateDentist;

public class CreateDentistCommandValidator : AbstractValidator<CreateDentistCommand>
{
    public CreateDentistCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required.");

        RuleFor(y => y.Email)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required.")
            .EmailAddress()
            .WithMessage("Invalid email address.");
    }
}