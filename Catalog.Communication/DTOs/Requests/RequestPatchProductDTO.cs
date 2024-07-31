using System.ComponentModel.DataAnnotations;

namespace Catalog.Communication.DTOs.Requests;

public class RequestPatchProductDTO
{
    [Range(1, 1000000, ErrorMessage = "The {0} must be between {1} and {2}")]
    public float Stock { get; set; }

    [StringLength(80, MinimumLength = 3, ErrorMessage = "The length of {0} must be between {2} and {1} characters")]
    public string Name {  get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "{0} cannot be longer than {1}")]
    public string? Description { get; set; }

    [Required]
    [Range(1, 10000, ErrorMessage = "The {0} must be between {1} and {2}")]
    public decimal Price { get; set; }
}
