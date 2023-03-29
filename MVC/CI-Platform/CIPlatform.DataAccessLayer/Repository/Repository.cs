using CIPlatform.DataAccessLayer.Data;
using CIPlatform.DataAccessLayer.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CIPlatform.DataAccessLayer.Repository;
public class Repository<T> : IRepository<T> where T : class
{
    private readonly CIDbContext _dbContext;
    internal DbSet<T> dbSet;
    public Repository(CIDbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet= _dbContext.Set<T>();
    }
    public void Add(T entity)
    {
        Console.WriteLine("Adding Entity DB");
        dbSet.Add(entity);
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public IEnumerable<T> GetAll(Func<T, bool> filter)
    {

        var query = dbSet.Where(filter);
        return query;
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet.Where(filter);
        return query.FirstOrDefault();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void AddRange(IEnumerable<T> list)
    {
        dbSet.AddRange(list);
    }

    public void RemoveRange(IEnumerable<T> list)
    {
        dbSet.RemoveRange(list);
    }
}
