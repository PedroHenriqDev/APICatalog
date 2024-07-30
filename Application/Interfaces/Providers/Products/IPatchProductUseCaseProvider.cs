using Application.Interfaces.UseCases.Products.Patch;

namespace Application.Interfaces.Providers.Products;

public interface IPatchProductUseCaseProvider
{
    IPatchProductUseCase PatchUseCase { get; }
}
