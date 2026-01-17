using System.ComponentModel.DataAnnotations;

namespace Domain.Utils.Classes;

public record PageOption
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Page { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    public string SortBy { get; set; } = "Id";
    public string SortDirection { get; set; } = "asc";
}
