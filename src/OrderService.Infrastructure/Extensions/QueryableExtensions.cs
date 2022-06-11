using System.Linq.Dynamic.Core;
using OrderService.Infrastructure.Interfaces;

namespace OrderService.Infrastructure.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Sort
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="sort"></param>
    /// <param name="asc"></param>
    /// <returns></returns>
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, ISort sort, bool asc = true)
    {
        try
        {
            if (string.IsNullOrEmpty(sort.SortBy))
                return queryable;

            return string.IsNullOrEmpty(sort.SortBy) ? queryable :
                asc == false ? queryable.OrderBy(sort.SortBy + " DESC") : queryable.OrderBy(sort.SortBy);
        }
        catch (Exception)
        {
            // ignored
        }

        return queryable;
    }


    /// <summary>
    /// Paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="current"></param>
    /// <param name="paging"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    public static IQueryable<T> Paging<T>(this IQueryable<T> current, IPaging paging, out int totalCount)
    {
        totalCount = current.Count();
        if (paging == null)
            return current;

        if (paging.PageNumber == 0 && paging.PageSize == 0)
            return current;
        paging.PageNumber = paging.PageNumber <= 0 ? 1 : paging.PageNumber;
        return current.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);
    }
}