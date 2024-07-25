using Communication.DTOs;
using Configuration.Resources;
using FluentValidation;

namespace Application.Validations.Categories;

public class CategoryValidation : AbstractValidator<CategoryDTO>
{
    public CategoryValidation()
    {
        RuleFor(category => category.Name)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.NAME_EMPTY);

        RuleFor(category => category.ImageUrl)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.IMAGE_EMPTY);
    }
}
