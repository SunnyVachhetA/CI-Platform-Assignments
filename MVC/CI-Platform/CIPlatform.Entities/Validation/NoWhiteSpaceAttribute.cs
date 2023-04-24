
using System.ComponentModel.DataAnnotations;
namespace CIPlatform.Entities.Validation;
public class NoWhiteSpaceAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;
        
        string stringValue = value.ToString();
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            return new ValidationResult("The field cannot be empty or contain only whitespace.");
        }

        return ValidationResult.Success;
    }
}

