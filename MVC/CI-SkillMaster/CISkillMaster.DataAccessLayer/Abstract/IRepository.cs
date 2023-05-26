using CISkillMaster.Entities.Request;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Abstract;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    IQueryable<T> GetAll();
    Task RemoveAsync(T entity);
    Task SaveAsync();
    Task UpdateAsync(T entity);
    Task<(int count, IEnumerable<T> list)> GetSortedPageList<TKey>(PaginationQuery pageQuery, Expression<Func<T, bool>>? filter = null, Expression<Func<T, TKey>>? orderBy = null);

    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
}
