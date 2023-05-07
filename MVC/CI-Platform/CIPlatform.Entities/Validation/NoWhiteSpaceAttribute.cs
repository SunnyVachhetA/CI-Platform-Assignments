
using System.ComponentModel.DataAnnotations;
namespace CIPlatform.Entities.Validation;
public class NoWhiteSpaceAttribute : ValidationAttribute
{
    public NoWhiteSpaceAttribute() : base(@"^\S+$")
    {
        ErrorMessage = "Input must not contain only whitespace characters";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Input must not contain only whitespace characters");
    }
}

