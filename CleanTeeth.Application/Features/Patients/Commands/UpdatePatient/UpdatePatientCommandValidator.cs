using FluentValidation;

namespace CleanTeeth.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
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