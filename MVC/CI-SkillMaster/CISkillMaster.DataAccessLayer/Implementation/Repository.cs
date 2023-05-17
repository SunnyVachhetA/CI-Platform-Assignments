using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CISkillMaster.DataAccessLayer.Implementation;

public class Repository<T>: IRepository<T>  where T : class
{
    protected readonly CIDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();   
    }   

    public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);

    public virtual IQueryable<T> GetAll() => _dbSet;

    public virtual Task RemoveAsync(T entity) => Task.Run(() => _dbSet.Remove(entity));
}
