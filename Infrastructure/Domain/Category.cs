using Infrastructure.Validations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infrastructure.Domain;

public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }

    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Name { get; set; }

    [Required]
    [StringLength(300)]
    [IsImageUrl]
    public string? ImageUrl { get; set; }

    [JsonIgnore]
    public ICollection<Product>? Products { get; set;}
}
