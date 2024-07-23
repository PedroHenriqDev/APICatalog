using APICatalog.Controllers;
using Microsoft.AspNetCore.Mvc;
using UnitTests.Tests.Products;

namespace UnitTests.Tests.Product.Actions;

public class GetProductsUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;

    public GetProductsUnitTests(ProductsControllerUnitTest controllerUnitTest) 
    {
        _controller = new ProductsController(controllerUnitTest.UnitOfWork, controllerUnitTest.Mapper); ;
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
    public async Task GetById_Return_NotFound() 
    {
        //Arrange
        int id = 0;

        //Act
        var data = await _controller.GetByIdAsync(id);

        //Assert
        var result = Assert.IsType<NotFoundObjectResult>(data.Result);
        Assert.Equal(404, result.StatusCode);
    }
}
