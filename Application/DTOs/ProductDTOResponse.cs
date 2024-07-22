using Infrastructure.Domain;

namespace Application.DTOs;

public class ProductDTOResponse
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public Category Category { get; set; }
}
