namespace CleanTeeth.Persistence.Utilities;

internal static class QueryableExtensions
{
    internal static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int page, int recordsPerPage)
    {
        return queryable
            .Skip((page - 1) * recordsPerPage)
            .Take(recordsPerPage);
    }
}