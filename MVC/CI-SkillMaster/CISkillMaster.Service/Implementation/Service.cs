using CISkillMaster.DataAccessLayer.Abstract;
using CISkillMaster.Entities.Request;
using CISkillMaster.Services.Abstract;
using System.Linq.Expressions;

namespace CISkillMaster.Services.Implementation;

public class Service<T> : IService<T> where T : class
{
    #region Properties
    private readonly IRepository<T> _repository;
    #endregion

    public Service(IRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public virtual async Task AddAsync(T entity) => await _repository.AddAsync(entity);

    public virtual async Task SaveAsync() => await _repository.SaveAsync();

    public async Task<(int count, IEnumerable<T> list)> GetSortedPageList<TKey>(PaginationQuery pageQuery, Expression<Func<T, bool>>? filter = null, Expression<Func<T, TKey>>? orderBy = null)
    {
        var result = await _repository.GetSortedPageList(pageQuery, filter, orderBy);

        return result;
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter) => await _repository.GetFirstOrDefaultAsync(filter);

    public async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

    public async Task RemoveAsync(T entity) => await _repository.RemoveAsync(entity);
}
