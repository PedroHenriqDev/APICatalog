using System.ComponentModel.DataAnnotations;

namespace APICatalog.Validations;

public class FirstLetterCapitalizedAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (!(value is string) || value is null)
        {
            return ValidationResult.Success;
        }
        if (char.IsUpper(value.ToString()[0])) 
        {
            return new ValidationResult("An error ocurred during validation, due to the first letter of text entered not being capitalized");
        }
        return ValidationResult.Success;
    }
}
