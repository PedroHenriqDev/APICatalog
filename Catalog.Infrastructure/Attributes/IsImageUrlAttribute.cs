using System.ComponentModel.DataAnnotations;

namespace Catalog.Infrastructure.Validations;

public class IsImageUrlAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string || value is null)
            return ValidationResult.Success;

        string message = "Invalid inserted image URL";

        string imageUrl = (string)value;
        if (imageUrl.Contains(".png", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult(message);

        if (imageUrl.Contains(".jpg", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult(message);

        if (imageUrl.Contains(".jpeg", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult(message);

        if (imageUrl.Contains(".webp", StringComparison.OrdinalIgnoreCase))
            return new ValidationResult(message);

        return ValidationResult.Success;
    }
}
