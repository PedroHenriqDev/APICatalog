﻿using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.UseCases.Categories.Get;
using Application.Pagination;
using AutoMapper;
using Communication.DTOs.Responses;

namespace Application.UseCases.Categories.Get;

public class GetCategoriesStatsUseCase : UseCase, IGetCategoriesStatsUseCase
{
    public GetCategoriesStatsUseCase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<ResponseCategoriesStatsDTO> ExecuteAsync(CategoriesParameters categoriesParameters)
    {
        var categories = await _unitOfWork.CategoryRepository.GetCategoriesWithProductsAsync(categoriesParameters);

        List<ResponseCategoryStatsDTO> responseCategoriesStats = [];

        foreach (var category in categories)
        {
            var productsPrices = category.Products != null && category.Products.Any()
                ? category.Products.Select(product => (double)product.Price).ToList()
                : new List<double>();

            responseCategoriesStats.Add(new ResponseCategoryStatsDTO
            {
                CategoryName = category.Name!,
                Mode = productsPrices.ModeWithLinq(),
                ProductsAvarage = productsPrices.CalculateAvarage(),
                QuantityOfProducts = category.Products!.Count,
                Variance = productsPrices.Variance(),
                StandartLeviation = productsPrices.StandartLeviation(),
            });
        }

        responseCategoriesStats.Ranking();

        var response = _mapper.Map<ResponseCategoriesStatsDTO>(responseCategoriesStats);

        return response;
    }
}