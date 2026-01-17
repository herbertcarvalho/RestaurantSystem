namespace Domain.Common;

public class PaginatedResponse<T>
{
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public ICollection<T> Items { get; set; }

    public static PaginatedResponse<T> Success(List<T> data, int count, int page, int pageSize)
    {

        return new PaginatedResponse<T>()
        {
            Items = data,
            TotalCount = count,
            CurrentPage = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
    }
}
