using Domain.Common;
using Domain.Utils.Classes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResponse<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, PageOption pageOption) where T : class
    {
        ArgumentNullException.ThrowIfNull(source);
        pageOption.Page = pageOption.Page == 0 ? 1 : pageOption.Page;
        pageOption.PageSize = pageOption.PageSize == 0 ? 10 : pageOption.PageSize;
        int count = await source
                                .CountAsync();
        pageOption.Page = pageOption.Page <= 0 ? 1 : pageOption.Page;

        if (!string.IsNullOrWhiteSpace(pageOption.SortBy))
            source = source.OrderByDynamic(pageOption.SortBy, pageOption.SortDirection);

        List<T> items = await source
                                .Skip((pageOption.Page - 1) * pageOption.PageSize)
                                .Take(pageOption.PageSize)
                                .ToListAsync();
        return PaginatedResponse<T>.Success(items, count, pageOption.Page, pageOption.PageSize);
    }

    private static IQueryable<T> OrderByDynamic<T>(
       this IQueryable<T> query,
       string sortBy,
       string sortDirection)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(param, sortBy);
        var lambda = Expression.Lambda(property, param);

        var method = sortDirection?.ToLower() == "desc"
            ? "OrderByDescending"
            : "OrderBy";

        var result = Expression.Call(
            typeof(Queryable),
            method,
            [typeof(T), property.Type],
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(result);
    }
}