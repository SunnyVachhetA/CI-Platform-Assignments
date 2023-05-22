using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.DataAccessLayer.Data;
using CISkillMaster.DataAccessLayer.Extension;
using CISkillMaster.Entities.Request;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Implementation;

public class Repository<T> : IRepository<T>  where T : class
{
    #region Properties
    protected readonly CIDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;
    #endregion

    #region Constructor
    public Repository(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();   
    }
    #endregion

    #region Generic implementation
    public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);

    public virtual IQueryable<T> GetAll() => _dbSet;

    public virtual Task RemoveAsync(T entity) => Task.Run(() => _dbSet.Remove(entity));

    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    public async Task<(int count, IEnumerable<T> list)> GetSortedPageList<TKey>(PaginationQuery pageQuery, Expression<Func<T, bool>>? filter = null, Expression<Func<T, TKey>>? orderBy = null)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        if (orderBy is not null)
            query = query.OrderBy(orderBy);

        int count = await query.CountAsync();

        query = query.ApplyPagination(pageQuery);

        return (count, query.AsEnumerable());
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
    #endregion
}
