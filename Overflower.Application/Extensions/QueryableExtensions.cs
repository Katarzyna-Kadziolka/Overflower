namespace Overflower.Application.Extensions;

public static class QueryableExtensions {
    public static IQueryable<T> GetPage<T>(this IQueryable<T> source, int page, int pageSize) {
        return source
            .Skip(pageSize * page - 1)
            .Take(pageSize);
    }
}
