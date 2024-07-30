﻿using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.Interfaces.UseCases.Products.Delete;
using Application.UseCases.Products.Delete;
using AutoMapper;

namespace Application.Providers.Products;

public class DeleteProductUseCaseProvider : BaseProvider, IDeleteProductUseCaseProvider
{
    private IDeleteProductByIdUseCase deleteByIdUseCase;

    public DeleteProductUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public IDeleteProductByIdUseCase DeleteByIdUseCase => deleteByIdUseCase 
        ??= new DeleteProductByIdUseCase(_unitOfWork, _mapper);
}
