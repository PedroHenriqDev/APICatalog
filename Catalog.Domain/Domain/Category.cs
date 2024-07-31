using System.Collections.ObjectModel;

namespace Catalog.Domain;

public sealed class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public ICollection<Product>? Products { get; set;}

    public Category()
    {
        Products = new Collection<Product>();
    }
}
