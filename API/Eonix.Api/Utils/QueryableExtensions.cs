namespace Eonix.Api.Utils;

public static class QueryableExtensions
{
    public static IQueryable<T> Apply<T>(this IQueryable<T> query, IFilter<T> filter)
    {
        return filter.ApplyTo(query);
    }
}
