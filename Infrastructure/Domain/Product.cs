using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Infrastructure.Domain;

public class Product : IValidatableObject
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    //[FirstLetterCapitalized(ErrorMessage = "The first letter of the {0} must be capitalized")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters")]
    public string? Name { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} cannot be longer than {1}")]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    [Range(1, 10000, ErrorMessage = "The {0} must be between {1} and {2}")]
    public decimal Price {  get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl {  get; set; }

    [Required]
    //[Range(1, 1000000, ErrorMessage = "The {0} must be between {1} and {2}")]
    public float Stock {  get; set; }
    public DateTime DateRegister { get; set; }
    public int CategoryId {  get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Name)) 
        {
            if (!Char.IsUpper(this.Name[0])) 
            {
                yield return new ValidationResult($"The first letter of the product must be capitalized", new[] 
                {
                    nameof(this.Name)
                });
            }
        }

        if(this.Stock <= 0) 
        {
            yield return new ValidationResult($"The stock must be greater than 0", new[] 
            {
                nameof(this.Stock)
            });
        }
    }
}
