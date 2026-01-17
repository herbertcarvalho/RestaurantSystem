using System.ComponentModel.DataAnnotations;

namespace Domain.Utils.Classes;

public record PageOption
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; } = 10;

    public string SortBy { get; set; } = "Id";
    public string SortDirection { get; set; } = "asc";
}
