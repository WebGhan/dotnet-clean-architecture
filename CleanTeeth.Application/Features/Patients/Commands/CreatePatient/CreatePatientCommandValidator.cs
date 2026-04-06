using FluentValidation;

namespace CleanTeeth.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required.");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("The field {PropertyName} is required.")
            .EmailAddress()
            .WithMessage("Invalid email address.");
    }
}  