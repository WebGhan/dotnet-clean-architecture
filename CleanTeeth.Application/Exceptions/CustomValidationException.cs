using FluentValidation.Results;

namespace CleanTeeth.Application.Exceptions;

public class CustomValidationException: Exception
{
    public List<string> ValidationErrors { get; set; } = [];

    public CustomValidationException(string message)
    {
        ValidationErrors.Add(message);
    }

    public CustomValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
}