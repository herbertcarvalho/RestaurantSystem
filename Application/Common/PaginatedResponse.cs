namespace Application.Common;

public class PaginatedResponse<T>
{
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public ICollection<T> Items { get; set; }
}
