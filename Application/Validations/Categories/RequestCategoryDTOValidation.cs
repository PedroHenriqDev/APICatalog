using Communication.DTOs.Requests;
using Configuration.Resources;
using FluentValidation;

namespace Application.Validations.Categories;

public class RequestPatchCategoryDTOValidation : AbstractValidator<RequestPatchCategoryDTO>
{
    public RequestPatchCategoryDTOValidation()
    {

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.NAME_EMPTY);

        RuleFor(request => request.ImageUrl)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.IMAGE_EMPTY);
    }
}
