﻿using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.Interfaces.UseCases.Categories.Put;
using Application.UseCases.Categories.Post;
using Application.UseCases.Categories.Put;
using Application.Validations.Categories;
using AutoMapper;

namespace Application.Providers.Categories;

public class PutCategoryUseCaseProvider : BaseProvider, IPutCategoryUseCaseProvider
{
    private readonly CategoryValidation _validation;

    private IUpdateCategoryUseCase updateCategoryUseCase;

    public PutCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidation validation) : base(unitOfWork, mapper)
    {
        _validation = validation;
    }

    public IUpdateCategoryUseCase UpdateUseCase => updateCategoryUseCase
        ??= new UpdateCategoryUseCase(_unitOfWork, _mapper, _validation);
}