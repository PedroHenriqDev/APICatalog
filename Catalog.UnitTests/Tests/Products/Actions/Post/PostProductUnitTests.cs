using Catalog.API.Controllers;
using Catalog.Application.Interfaces.Managers;
using Catalog.Communication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Catalog.UnitTests.Tests.Products.Actions.Post;

public class PostProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;
    private readonly Mock<IProductUseCaseManager> _useCaseManagerMock;

    public PostProductUnitTests(ProductsControllerUnitTest controller)
    {
        _useCaseManagerMock = new Mock<IProductUseCaseManager>();
        _useCaseManagerMock.Setup(manager => manager.PostProvider.RegisterUseCase.ExecuteAsync(It.IsAny<ProductDTO>()))
            .ReturnsAsync((ProductDTO productDTO) => new ProductDTO
            {
                Category = productDTO.Category,
                CategoryId = productDTO.CategoryId,
                DateRegister = productDTO.DateRegister,
                Description = productDTO.Description,
                ImageUrl = productDTO.ImageUrl,
                Name = productDTO.Name,
                Price = productDTO.Price,
                ProductId = productDTO.ProductId,
                Stock = productDTO.Stock
            });

        _controller = new ProductsController(controller.Mapper, _useCaseManagerMock.Object);
    }

    [Fact]
    public async Task Post_Return_Created()
    {
        var prod = new ProductDTO
        {
            CategoryId = 1,
            DateRegister = DateTime.UtcNow,
            ImageUrl = "testproduct.png",
            Description = "Test",
            Stock = 1,
            Name = "Test",
            Price = 1,
        };

        var data = await _controller.PostAsync(prod);

        var result = Assert.IsType<CreatedAtRouteResult>(data.Result);
        Assert.NotNull(result.Value);
        Assert.Equal(201, result.StatusCode);
    }
}
