using Catalog.Application.Interfaces.UseCases.Products.Patch;

namespace Catalog.Application.Interfaces.Providers.Products;

public interface IPatchProductUseCaseProvider
{
    IPatchProductUseCase PatchUseCase { get; }
}
