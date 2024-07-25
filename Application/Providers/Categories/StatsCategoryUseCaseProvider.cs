using Application.Interfaces;
using Application.Interfaces.Providers.Categories;
using Application.UseCases.Categories.Stats;
using AutoMapper;

namespace Application.Providers.Categories;

public class StatsCategoryUseCaseProvider : BaseProvider, IStatsCategoryUseCaseProvider
{
    private CalculateStatsUseCase statsUseCase;

    public StatsCategoryUseCaseProvider(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public CalculateStatsUseCase StatsUseCase => statsUseCase ??=
        new CalculateStatsUseCase(_unitOfWork, _mapper);
}
