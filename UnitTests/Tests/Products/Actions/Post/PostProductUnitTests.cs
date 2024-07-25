using APICatalog.Controllers;
using Communication.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Tests.Products.Actions.Post;

public class PostProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;

    public PostProductUnitTests(ProductsControllerUnitTest controller) 
    {
        _controller = new ProductsController(controller.Mapper, controller.UseCaseManager);
    }

    [Fact]  
    public async Task Post_Return_Created() 
    {
        var prod = new ProductDTO
        {
            CategoryId = 1,
            DateRegister = DateTime.UtcNow,
            ImageUrl = "product.png",
            Description = "Test",
            Stock = 2,
            Name = "Test",
            Price = 2,
        };

        var data = await _controller.PostAsync(prod);

        var result = Assert.IsType<CreatedAtRouteResult>(data.Result);
        Assert.Equal(201, result.StatusCode);
    }

    [Fact]
    public async Task Post_Return_BadRequest() 
    {
        ProductDTO prod = null;

        var data = await _controller.PostAsync(prod);

        var result = Assert.IsType<BadRequestResult>(data.Result);
        Assert.Equal(400, result.StatusCode);
    }
}
