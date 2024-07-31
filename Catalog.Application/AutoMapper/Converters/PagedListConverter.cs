using Catalog.Application.Pagination;
using AutoMapper;

namespace Catalog.Application.AutoMapper.Converters;

public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
where TSource : class where TDestination : class
{
    private readonly IMapper _mapper;

    public PagedListConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
    {
        var items = _mapper.Map<IEnumerable<TDestination>>(source);

        return PagedList<TDestination>.ToPagedList(items.AsQueryable(), source.CurrentPage, source.PageSize);
    }
}
