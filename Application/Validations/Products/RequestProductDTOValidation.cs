﻿using Communication.DTOs.Requests;
using Configuration.Resources;
using FluentValidation;

namespace Application.Validations.Products;

public class RequestProductDTOValidation : AbstractValidator<RequestPatchProductDTO>
{

    public RequestProductDTOValidation()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.NAME_EMPTY);

        RuleFor(request => request.Description)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.DESCRIPTION_EMPTY);

        RuleFor(request => request.Price)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ErrorMessagesResource.INVALID_PRICE);

        RuleFor(request => request.Stock)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ErrorMessagesResource.INVALID_STOCK);
    }
}
