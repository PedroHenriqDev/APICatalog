using Application.UseCases.Products.Patch;

namespace Application.Interfaces.Providers.Products;

public interface IPatchProductUseCaseProvider
{
    PatchProductUseCase PatchUseCase { get; }
}
