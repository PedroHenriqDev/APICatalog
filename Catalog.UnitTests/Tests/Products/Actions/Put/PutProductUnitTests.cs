using Catalog.API.Controllers;
using Catalog.Application.Interfaces.Managers;
using Catalog.Communication.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Catalog.UnitTests.Tests.Products.Actions.Put;

public class PutProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;
    private readonly Mock<IProductUseCaseManager> _useCaseManagerMock;

    public PutProductUnitTests(ProductsControllerUnitTest controller)
    {
        _useCaseManagerMock = new Mock<IProductUseCaseManager>();
        _useCaseManagerMock.Setup(manager => manager.PutProvider.PutUseCase.ExecuteAsync(It.IsAny<int>(), It.IsAny<ProductDTO>())).ReturnsAsync((int id, ProductDTO productDTO) =>
            new ProductDTO
            {
                Category = productDTO.Category,
                Description = productDTO.Description,
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                ImageUrl = productDTO.ImageUrl,
                ProductId = id,
                DateRegister = productDTO.DateRegister,
                CategoryId = productDTO.CategoryId,
            }
        );

        _controller = new ProductsController(controller.Mapper, _useCaseManagerMock.Object);
    }

    [Fact]
    public async Task Put_Return_OkResult()
    {
        int prodId = 21;

        var product = new ProductDTO
        {
            ProductId = prodId,
            Name = "Changed prod",
            DateRegister = DateTime.UtcNow,
            Description = "Changed description",
            Price = 1,
            Stock = 1,
            ImageUrl = "changedprod.jpg",
            CategoryId = 1
        };

        var data = await _controller.PutAsync(prodId, product);

        var result = Assert.IsType<OkObjectResult>(data.Result);
        Assert.Equal(200, result.StatusCode);
    }
}
