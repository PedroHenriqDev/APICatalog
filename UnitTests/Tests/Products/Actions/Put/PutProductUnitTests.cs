using APICatalog.Controllers;

namespace UnitTests.Tests.Products.Actions.Put;

public class PutProductUnitTests : IClassFixture<ProductsControllerUnitTest>
{
    private readonly ProductsController _controller;

    public PutProductUnitTests(ProductsControllerUnitTest controller)
    {
        _controller = new ProductsController(controller.Mapper, controller.UseCaseManager);
    }
}
