namespace Catalog.Application.Pagination;

public class CategoriesFilterNameParameters : QueryStringParameters
{
    public string? CategoryName { get; set; }
}
