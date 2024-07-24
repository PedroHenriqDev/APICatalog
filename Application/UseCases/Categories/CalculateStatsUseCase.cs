using Application.Extensions;
using Application.Pagination;
using Application.Repositories;
using AutoMapper;
using Communication.DTOs.Responses;

namespace Application.UseCases.Categories;

public class CalculateStatsUseCase
{
    private readonly CategoryRepository _repository;
    private readonly IMapper _mapper;

    public CalculateStatsUseCase(CategoryRepository respository, IMapper mapper)
    {
        _repository = respository;
        _mapper = mapper;
    }

    public async Task<ResponseCategoriesStatsDTO> ExecuteAsync(CategoriesParameters categoriesParameters)
    {
        var categories = await _repository.GetCategoriesWithProductsAsync(categoriesParameters);

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
