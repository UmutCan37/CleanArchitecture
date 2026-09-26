using CleanArchitecture.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginationResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginationResult<T>(items, totalCount, pageNumber, pageSize);
    }
}