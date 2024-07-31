namespace Catalog.Application.Pagination;

public class ProductsFilterPriceParameters : QueryStringParameters
{
    public decimal? Price { get; set; }
    public string? PriceParameter { get; set;}
}
