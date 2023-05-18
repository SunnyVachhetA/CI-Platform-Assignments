using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Extension;

public static class FilterExtension
{
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter)
    {
        if(filter is null) throw new ArgumentNullException(nameof(filter), "Filter expressin cannot be null.");
        return
            query.Where(filter);
    }
}
