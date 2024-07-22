using Infrastructure.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ProductDTO
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
    public decimal Price { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }

    [Required]
    [Range(1, 1000000, ErrorMessage = "The {0} must be between {1} and {2}")]
    public float Stock { get; set; }
    public DateTime DateRegister { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
