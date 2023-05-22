using CISkillMaster.Entities.Request;

namespace CISkillMaster.DataAccessLayer.Extension;

public static class PaginationExtension
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationQuery paginationQuery)
    {
        int pageSize = paginationQuery.PageSize;
        int pageNumber = paginationQuery.PageNumber;

        if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
        if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than or equal to 1.");

        int skip = (pageNumber - 1) * pageSize;

        return
            query
            .Skip(skip)
            .Take(pageSize);
    }
}
