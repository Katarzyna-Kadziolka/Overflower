using Overflower.Application.Paging;

namespace Overflower.Application.Extensions;

public static class QueryableExtensions {
    public static IQueryable<T> GetPage<T>(this IQueryable<T> source, int page, int pageSize) {
        return source
            .Skip(pageSize * page - 1)
            .Take(pageSize);
    }

    public static PageResult<T> ToPageResult<T>(this IQueryable<T> source, int currentPage, int totalPages) where T: class {
        return new PageResult<T> {
            Items = source.ToList(),
            CurrentPage = currentPage,
            TotalPages = totalPages
        };
    }
}
