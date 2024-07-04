using System.ComponentModel.DataAnnotations;

namespace APICatalog.Validations;

public class IsImageUrlAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string || value is null)
            return ValidationResult.Success;

        string imageUrl = (string)value;
        if (imageUrl.Contains(".png", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult("Invalid inserted image URL");

        if (imageUrl.Contains(".jpg", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult("Invalid inserted image URL");

        if (imageUrl.Contains(".jpeg", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult("Invalid inserted image URL");

        if (imageUrl.Contains(".webp", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult("Invalid inserted image URL");

        return ValidationResult.Success;
    }
}
