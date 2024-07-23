using Infrastructure.Domain;

namespace Application.Mapper;

static public class ProductMapper
{
    public static Product MapProductWithCategory(this Product product) 
    {
        return new Product
        {
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Name = product.Name,
            ProductId = product.ProductId,
            Price = product.Price,
            Description = product.Description,
            DateRegister = product.DateRegister,
            Stock = product.Stock,
            Category = new Category()
            {
                CategoryId = product.Category.CategoryId,
                Name = product.Category.Name,
                ImageUrl = product.Category.ImageUrl,
            }
        };
    }
}
