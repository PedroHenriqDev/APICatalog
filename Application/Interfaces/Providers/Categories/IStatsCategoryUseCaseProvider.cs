using Application.UseCases.Categories.Stats;

namespace Application.Interfaces.Providers.Categories;

public interface IStatsCategoryUseCaseProvider
{
    CalculateStatsUseCase StatsUseCase { get; }
}
