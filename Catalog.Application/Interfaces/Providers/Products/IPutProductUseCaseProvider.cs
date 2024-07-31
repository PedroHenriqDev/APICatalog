using Catalog.Application.Interfaces.UseCases.Products.Put;
using Catalog.Application.UseCases.Products.Put;

namespace Catalog.Application.Interfaces.Providers.Products;

public interface IPutProductUseCaseProvider
{
    IUpdateProductUseCase PutUseCase {  get; }
}
