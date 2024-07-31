using Catalog.Communication.DTOs;
using Catalog.Configuration.Resources;
using FluentValidation;

namespace Catalog.Application.Validations.Products;

public class ProductValidation : AbstractValidator<ProductDTO>
{
    public ProductValidation()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.NAME_EMPTY);

        RuleFor(product => product.Description)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.DESCRIPTION_EMPTY);

        RuleFor(product => product.Price)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ErrorMessagesResource.INVALID_PRICE);

        RuleFor(product => product.Stock)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ErrorMessagesResource.INVALID_STOCK);
    }
}
