using CISkillMaster.Entities.Request;
using System.Linq.Expressions;

namespace CISkillMaster.Services.Abstract;

public interface IService<T> where T : class
{
    Task AddAsync(T entity);
    Task SaveAsync();
    Task<(int count, IEnumerable<T> list)> GetSortedPageList<TKey>(PaginationQuery pageQuery, Expression<Func<T, bool>>? filter = null, Expression<Func<T, TKey>>? orderBy = null);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
}
