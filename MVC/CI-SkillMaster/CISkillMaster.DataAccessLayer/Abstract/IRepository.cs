using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Abstract;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    IQueryable<T> GetAll();

    Task RemoveAsync(T entity);
}
