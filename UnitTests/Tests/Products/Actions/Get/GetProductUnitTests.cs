using APICatalog.Controllers;
using Application.Pagination;
using Communication.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Tests.Products.Actions.Get;

public class GetProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;

    public GetProductUnitTests(ProductsControllerUnitTest controllerUnitTest)
    { 
         ControllerContext controllerContext = new ControllerContext();
         controllerContext.HttpContext = new DefaultHttpContext();
        _controller = new ProductsController(controllerUnitTest.Mapper, controllerUnitTest.UseCaseManager) 
        {
            ControllerContext = controllerContext,
        };
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
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public void GetProducts_Return_OkResult()
    {

        var productsDTO = new List<ProductDTO>
        {
            new ProductDTO
            {
                CategoryId = 1,
                DateRegister = DateTime.Now,
                Name = "Test",
                Stock = 2,
                Price = 3,
                ProductId = 2,
                Description = "Test description",
                ImageUrl = "test.jpg"
            }
        };

        var pagedProductsDTO = PagedList<ProductDTO>.ToPagedList(productsDTO.AsQueryable(), 1, 1);

        var data = _controller.GetProductsMetaData(pagedProductsDTO);

        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.NotNull(result.Value);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}
