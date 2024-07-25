using Application.UseCases.Products.Put;

namespace Application.Interfaces.Providers.Products;

public interface IPutProductUseCaseProvider
{
    PutProductUseCase PutUseCase {  get; }
}
