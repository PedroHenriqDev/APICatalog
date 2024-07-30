using APICatalog.Controllers;
using Application.Interfaces.Managers;
using Communication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Tests.Products.Actions.Delete;

public class DeleteProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;
    private readonly Mock<IProductUseCaseManager> _useCaseManagerMock;

    public DeleteProductUnitTests(ProductsControllerUnitTest controller)
    {
        _useCaseManagerMock = new Mock<IProductUseCaseManager>();
        _useCaseManagerMock.Setup(manager => manager.DeleteProvider.DeleteByIdUseCase.ExecuteAsync(It.IsAny<int>()))
            .ReturnsAsync(new ProductDTO
            {
                Name = "Deleted product",
                Price = 1,
                ProductId = 1,
                CategoryId = 1,
                Stock = 1,
                Description = "Deleted product",
                ImageUrl = "Deleted product",
                DateRegister = DateTime.Now,

            });
        _controller = new ProductsController(controller.Mapper, _useCaseManagerMock.Object);
    }

    [Fact]
    public async Task Delete_Return_OkResult()
    {
        var productId = 3;

        var data = await _controller.DeleteAsync(productId);

        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.NotNull(result.Value);
        Assert.Equal(200, result.StatusCode);
    }
}
