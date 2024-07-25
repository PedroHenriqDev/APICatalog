using APICatalog.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Tests.Products.Actions.Get;

public class GetProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;

    public GetProductUnitTests(ProductsControllerUnitTest controllerUnitTest)
    {
        _controller = new ProductsController(controllerUnitTest.Mapper, controllerUnitTest.UseCaseManager);
    }

    [Fact]
    public async Task GetById_Return_OkResult()
    {
        //Arrange
        int id = 2;

        //Act
        var data = await _controller.GetByIdAsync(id);

        //Assert
        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task GetById_Return_BadRequest() 
    {
        int id = 0;

        var data = await _controller.GetByIdAsync(id);

        var result = Assert.IsType<ObjectResult>(data.Result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task GetById_Return_NotFound()
    {
        //Arrange
        int id = 123818;

        //Act
        var data = await _controller.GetByIdAsync(id);

        //Assert
        var result = Assert.IsType<ObjectResult>(data.Result);
        Assert.Equal(404, result.StatusCode);
    }
}
