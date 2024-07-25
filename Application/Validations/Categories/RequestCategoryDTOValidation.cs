using Communication.DTOs.Requests;
using Configuration.Resources;
using FluentValidation;

namespace Application.Validations.Categories;

public class RequestPatchCategoryDTOValidation : AbstractValidator<RequestPatchCategoryDTO>
{
    public RequestPatchCategoryDTOValidation()
    {

        RuleFor(category => category.Name)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.NAME_EMPTY);

        RuleFor(category => category.ImageUrl)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.IMAGE_EMPTY);
    }
}
