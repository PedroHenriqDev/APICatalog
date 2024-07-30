using Application.Interfaces.UseCases.Products.Put;
using Application.UseCases.Products.Put;

namespace Application.Interfaces.Providers.Products;

public interface IPutProductUseCaseProvider
{
    IUpdateProductUseCase PutUseCase {  get; }
}
