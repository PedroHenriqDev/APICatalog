namespace APICatalog.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int TotalPages {  get; private set; }
    public int PageSize { get; private set;}
    public int CurrentPage { get; private set; }
    public int TotalCount { get; private set;}

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize) 
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = source.Count();
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    }
}
