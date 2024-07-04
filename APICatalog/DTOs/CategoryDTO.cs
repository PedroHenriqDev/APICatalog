using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICatalog.DTOs;

public class CategoryDTO
{
    public CategoryDTO()
    {
        Products = new Collection<ProductDTO>();
    }

    public int CategoryId { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 1, ErrorMessage = "The {0} must be {1} to {2} characters")]
    public string? Name { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 1, ErrorMessage = "The {0} must be {1} to {2} characters")]
    public string? ImageUrl { get; set; }

    public ICollection<ProductDTO>? Products { get; set; }
}
